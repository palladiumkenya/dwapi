using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors;
using Dwapi.ExtractsManagement.Core.Interfaces.Extratcors.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Reader.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Cbs;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Extractors.Cbs
{
    public class MasterPatientIndexSourceExtractor : IMasterPatientIndexSourceExtractor
    {
        private readonly IMasterPatientIndexReader _reader;
        private readonly IMediator _mediator;
        private readonly ITempMasterPatientIndexRepository _extractRepository;

        public MasterPatientIndexSourceExtractor(IMasterPatientIndexReader reader, IMediator mediator, ITempMasterPatientIndexRepository extractRepository)
        {
            _reader = reader;
            _mediator = mediator;
            _extractRepository = extractRepository;
        }

        public async Task<int> Extract(DbExtract extract, DbProtocol dbProtocol)
        {
            int batch = 500;
            // TODO: Notify started...


            var list = new List<TempMasterPatientIndex>();

            int count = 0;

            using (var rdr = await _reader.ExecuteReader(dbProtocol, extract))
            {
                while (rdr.Read())
                {
                    count++;
                    // AutoMapper profiles
                    var extractRecord = Mapper.Map<IDataRecord, TempMasterPatientIndex>(rdr);
                    extractRecord.Id = LiveGuid.NewGuid();
                    list.Add(extractRecord);

                    if (count == batch)
                    {
                        // TODO: batch and save
                        _extractRepository.BatchInsert(list);

                        count = 0;

                        try
                        {
                            DomainEvents.Dispatch(
                                new ExtractActivityNotification(new DwhProgress(
                                    nameof(MasterPatientIndex),
                                    "loading...",
                                    list.Count, 0, 0, 0, 0)));
                        }
                        catch (Exception e)
                        {
                            Log.Error(e,"Notification error");
                            
                        }

                        list=new List<TempMasterPatientIndex>();
                    }

                    // TODO: Notify progress...


                }

                if (count > 0)
                {
                    _extractRepository.BatchInsert(list);
                }
                _extractRepository.CloseConnection();
            }

            try
            {
                // TODO: Notify Completed;
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(new DwhProgress(
                        nameof(MasterPatientIndex),
                        "loaded",
                        list.Count, 0, 0, 0, 0)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Notification error");

            }

            return list.Count;
        }
    }
}
