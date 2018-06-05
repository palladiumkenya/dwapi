using System;
using System.Linq;
using System.Threading.Tasks;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository;
using Dwapi.ExtractsManagement.Core.Interfaces.Services;
using Dwapi.SettingsManagement.Core.Model;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Dwapi.UploadManagement.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Dwapi.Controller
{
    [Produces("application/json")]
    [Route("api/ExtractSender")]
    public class ExtractSenderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IPsmartSendService _psmartSendService;
        private readonly IPsmartExtractService _psmartExtractService;
        private readonly IPsmartStageRepository _psmartStageRepository;
        private readonly IExtractHistoryRepository _extractHistoryRepository;

        public ExtractSenderController(IPsmartSendService psmartSendService,
            IPsmartStageRepository psmartStageRepository, IExtractHistoryRepository extractHistoryRepository, IPsmartExtractService psmartExtractService)
        {
            _psmartSendService = psmartSendService;
            _psmartStageRepository = psmartStageRepository;
            _extractHistoryRepository = extractHistoryRepository;
            _psmartExtractService = psmartExtractService;
        }

        // GET: api/ExtractSender/status/id
        [HttpGet("status/{id}")]
        public IActionResult GetStatus(Guid id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest();
            try
            {
                var eventExtract = _psmartExtractService.GetStatus(id);
                if (null == eventExtract)
                    return NotFound();

                return Ok(eventExtract);
            }
            catch (Exception e)
            {
                var msg = $"Error loading {nameof(Extract)}(s)";
                Log.Error(msg);
                Log.Error($"{e}");
                return StatusCode(500, msg);
            }
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
                    //check if sending

                    var extractHistory = _psmartExtractService.HasStarted(packageDTO.ExtractId.Value,ExtractStatus.Sending,ExtractStatus.Sent);

                    if (null != extractHistory && extractHistory.IsSending())
                    {
                        //prevent sending
                        var eventHistory = _psmartExtractService.GetStatus(packageDTO.ExtractId.Value);
                        if (null != eventHistory)
                            return Ok(new
                            {
                                IsComplete = false,
                                IsSending = true,
                                eEvent = eventHistory
                            });
                    }


                    var psmartdata = _psmartStageRepository.GetAll().ToList();
                    if (psmartdata.Count > 0)
                    {
                        var psmartDTO = psmartdata.Select(x => new SmartMessage {Eid =x.EId, Id = x.Uuid, PayLoad = x.Shr}).ToList()
                            .ToList();
                        var bag = new SmartMessageBag(psmartDTO);
                        _extractHistoryRepository.UpdateStatus(packageDTO.ExtractId.Value, ExtractStatus.Sending);
                        var response = await _psmartSendService.SendAsync(packageDTO, bag);
                   
                        if (null != response)
                        {
                            if (response.IsValid())
                            {
                                // update sent
                                _extractHistoryRepository.UpdateStatus(packageDTO.ExtractId.Value, ExtractStatus.Sent,bag.Message.Count);
                                _psmartExtractService.Complete(packageDTO.ExtractId.Value);
                                _psmartStageRepository.UpdateStatus(bag.Message.Select(x=>x.Eid),response.IsValid(),response.RequestId);

                                var history = _psmartExtractService.GetStatus(packageDTO.ExtractId.Value);

                                if (null != history)
                                    return Ok(new
                                    {
                                        IsComplete = true,
                                        IsSending = false,
                                        eEvent = history
                                    });


                                return Ok(response);
                            }
                                
                        }
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
