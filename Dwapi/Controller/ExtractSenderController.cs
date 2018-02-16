using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SettingsManagement.Core.Interfaces.Repositories;
using Dwapi.SettingsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services.Psmart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/ExtractSender")]
    public class ExtractSenderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPsmartSendService _psmartSendService;
        private readonly IPsmartStageRepository _psmartStageRepository;

        public ExtractSenderController(IPsmartSendService psmartSendService,
            IPsmartStageRepository psmartStageRepository)
        {
            _psmartSendService = psmartSendService;
            _psmartStageRepository = psmartStageRepository;
        }

        // POST: api/ExtractSender
        [HttpPost]
        public async Task<IActionResult> Send([FromBody] SendPackageDTO packageDTO)
        {
            if (!packageDTO.IsValid())
                return BadRequest();

            try
            {
                if (packageDTO.Docket.IsSameAs("PSMART"))
                {
                    var psmartdata = _psmartStageRepository.GetAll().ToList();
                    if (psmartdata.Count > 0)
                    {
                        var psmartDTO = psmartdata.Select(x => new SmartMessage {Id = x.Uuid, PayLoad = x.Shr}).ToList()
                            .ToList();
                        var bag = new SmartMessageBag(psmartDTO);
                        var response = await _psmartSendService.SendAsync(packageDTO, bag);
                        return Ok(response);
                    }
                }

                return BadRequest();

            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
        }

    }
}
