using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Commands;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Mts;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.ExtractsManagement.Core.Model.Destination.Mts.Dto;
using Dwapi.Hubs.Mgs;
using Dwapi.Models;
using Dwapi.SettingsManagement.Core.Application.Checks.Commands;
using Dwapi.SettingsManagement.Core.Application.Checks.Queries;
using Dwapi.SettingsManagement.Core.Application.Metrics.Events;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mgs;
using Dwapi.UploadManagement.Core.Interfaces.Services.Mts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/Mts")]
    public class MtsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMediator _mediator;
        private readonly IMtsSendService _mtsSendService;
        private readonly IEmrManagerService _emrManagerService;
        private string _version;
        private readonly IIndicatorExtractRepository _extractRepository;

        public MtsController(IMediator mediator, IMtsSendService mtsSendService, IEmrManagerService emrManagerService, IIndicatorExtractRepository extractRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mtsSendService = mtsSendService;
            _emrManagerService = emrManagerService;
            _extractRepository = extractRepository;
            var ver = GetType().Assembly.GetName().Version;
            _version = $"{ver.Major}.{ver.Minor}.{ver.Build}";
        }

        [HttpGet("load")]
        public async Task<IActionResult> Load()
        {
            var emr = _emrManagerService.GetDefault();

            LoadMtsExtracts request = new LoadMtsExtracts()
            {
                LoadMtsFromEmrCommand = new LoadMtsFromEmrCommand()
                {
                    Extracts = emr.Extracts.Where(x => x.DocketId == "MTS").ToList(),
                    DatabaseProtocol = emr.DatabaseProtocols.FirstOrDefault()
                }
            };

            string version = GetType().Assembly.GetName().Version.ToString();
            await _mediator.Publish(new ExtractLoaded("MetricService", version));

            var result = await _mediator.Send(request.LoadMtsFromEmrCommand, HttpContext.RequestAborted);

            await _mediator.Send(new InitAppVerCheck());

            await _mediator.Send(new PerformCheck(emr.Id, CheckStage.PreSend, version));

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> Summary()
        {
            var emr = _emrManagerService.GetDefault();

            var result = await _mediator.Send(new GetCheckSummary(), HttpContext.RequestAborted);

            return Ok(result.Value);
        }

        [HttpGet("indicator")]
        public IActionResult Indicator()
        {
            var emr = _extractRepository.GetAll().ToList();

            var result = Mapper.Map<List<IndicatorExtractDto>>(emr);

            return Ok(result.OrderBy(x=>x.Rank));
        }
    }
}
