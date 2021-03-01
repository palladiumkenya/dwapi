using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dwapi.ExtractsManagement.Core.Application.Events;
using Dwapi.ExtractsManagement.Core.Interfaces.Loaders.Dwh;
using Dwapi.ExtractsManagement.Core.Interfaces.Repository.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Destination.Dwh;
using Dwapi.ExtractsManagement.Core.Model.Source.Dwh;
using Dwapi.ExtractsManagement.Core.Notifications;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Events;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using MediatR;
using Serilog;

namespace Dwapi.ExtractsManagement.Core.Loader.Dwh
{
    public class ContactListingLoader : IContactListingLoader
    {
        private readonly IContactListingExtractRepository _ContactListingExtractRepository;
        private readonly ITempContactListingExtractRepository _tempContactListingExtractRepository;
        private readonly IMediator _mediator;

        public ContactListingLoader(IContactListingExtractRepository ContactListingExtractRepository, ITempContactListingExtractRepository tempContactListingExtractRepository, IMediator mediator)
        {
            _ContactListingExtractRepository = ContactListingExtractRepository;
            _tempContactListingExtractRepository = tempContactListingExtractRepository;
            _mediator = mediator;
        }

        public async Task<int> Load(Guid extractId, int found)
        {
            int count = 0;

            try
            {
                DomainEvents.Dispatch(
                    new ExtractActivityNotification(extractId, new DwhProgress(
                        nameof(ContactListingExtract),
                        nameof(ExtractStatus.Loading),
                        found, 0, 0, 0, 0)));


                StringBuilder query = new StringBuilder();
                query.Append($" SELECT s.* FROM {nameof(TempContactListingExtract)}s s");
                query.Append($" INNER JOIN PatientExtracts p ON ");
                query.Append($" s.PatientPK = p.PatientPK AND ");
                query.Append($" s.SiteCode = p.SiteCode ");

                const int take = 1000;
                var eCount = await  _tempContactListingExtractRepository.GetCount(query.ToString());
                var pageCount = _tempContactListingExtractRepository.PageCount(take, eCount);

                int page = 1;
                while (page <= pageCount)
                {
                    var tempContactListingExtracts =await
                        _tempContactListingExtractRepository.ReadAll(query.ToString(), page, take);

                    var batch = tempContactListingExtracts.ToList();
                    count += batch.Count;

                    //Auto mapper
                    var extractRecords = Mapper.Map<List<TempContactListingExtract>, List<ContactListingExtract>>(batch);
                    foreach (var record in extractRecords)
                    {
                        record.Id = LiveGuid.NewGuid();
                    }
                    //Batch Insert
                    var inserted = _ContactListingExtractRepository.BatchInsert(extractRecords);
                    if (!inserted)
                    {
                        Log.Error($"Extract {nameof(ContactListingExtract)} not Loaded");
                        return 0;
                    }
                    Log.Debug("saved batch");
                    page++;
                    DomainEvents.Dispatch(
                        new ExtractActivityNotification(extractId, new DwhProgress(
                            nameof(ContactListingExtract),
                            nameof(ExtractStatus.Loading),
                            found, count , 0, 0, 0)));
                }

                await _mediator.Publish(new DocketExtractLoaded("NDWH", nameof(ContactListingExtract)));

                return count;
            }
            catch (Exception e)
            {
                Log.Error(e, $"Extract {nameof(ContactListingExtract)} not Loaded");
                return 0;
            }
        }
    }
}
