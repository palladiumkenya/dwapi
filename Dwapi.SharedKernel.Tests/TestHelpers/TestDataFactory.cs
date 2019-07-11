using System.Collections.Generic;
using System.Linq;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.ExtractsManagement.Core.Model.Destination.Hts;
using Dwapi.SharedKernel.Exchange;
using Dwapi.SharedKernel.Model;
using Dwapi.UploadManagement.Core.Exchange.Cbs;
using Dwapi.UploadManagement.Core.Exchange.Dwh;
using Dwapi.UploadManagement.Core.Exchange.Hts;
using Dwapi.UploadManagement.Core.Model.Cbs.Dtos;
using Dwapi.UploadManagement.Core.Model.Dwh;
using FizzWare.NBuilder;

namespace Dwapi.SharedKernel.Tests.TestHelpers
{
    public class TestDataFactory
    {

        public static DwhManifestMessageBag DwhManifestMessageBag(int count, params int[] siteCodes)
        {
            var list = Builder<DwhManifestMessageBag>.CreateNew().Build();

            list.Messages.AddRange(DwhManifestMessages(count, siteCodes));

            return list;
        }

        private static List<DwhManifestMessage> DwhManifestMessages(int count, params int[] siteCodes)
        {
            var list = new List<DwhManifestMessage>();

            foreach (var siteCode in siteCodes)
            {
                var manifests = Builder<DwhManifest>.CreateListOfSize(count).All()
                    .With(x => x.SiteCode = siteCode)
                    .With(x => x.PatientPks =new List<int>{1,2})
                    .Build()
                    .ToList();
                foreach (var manifest in manifests)
                {
                    list.Add(new DwhManifestMessage(manifest));
                }
            }
            return list;
        }


        public static ManifestMessageBag ManifestMessageBag(int count,params int[] siteCodes)
        {
            var list = Builder<ManifestMessageBag>.CreateNew().Build();

            list.Messages.AddRange(ManifestMessages(count,siteCodes));

            return list;
        }

        private static List<ManifestMessage> ManifestMessages(int count, params int[] siteCodes)
        {
            var list=new List<ManifestMessage>();

            foreach (var siteCode in siteCodes)
            {
                var manifests = Builder<Manifest>.CreateListOfSize(count).All().With(x => x.SiteCode = siteCode).Build()
                    .ToList();
                foreach (var manifest in manifests)
                {
                    list.Add(new ManifestMessage(manifest));
                }
            }
            return list;
        }

        public static MpiMessageBag MpiMessageBag(int count, params int[] siteCodes)
        {
            var list = Builder<MpiMessageBag>.CreateNew().Build();

            list.Messages.AddRange(MpiMessages(count, siteCodes));

            return list;
        }

        private static List<MpiMessage> MpiMessages(int count, params int[] siteCodes)
        {
            var list = new List<MpiMessage>();

            foreach (var siteCode in siteCodes)
            {
                var masterPatientIndices = Builder<MasterPatientIndexDto>.CreateListOfSize(count).All().With(x => x.SiteCode = siteCode).Build()
                    .ToList();
                list.Add(new MpiMessage(masterPatientIndices));

            }
            return list;
        }

        public static HtsMessageBag HtsMessageBag(int count, params int[] siteCodes)
        {
            var list = Builder<HtsMessageBag>.CreateNew().Build();

            list.Messages.AddRange(HtsMessages(count, siteCodes));

            return list;
        }

        private static List<HtsMessage> HtsMessages(int count, params int[] siteCodes)
        {
            var list = new List<HtsMessage>();

            foreach (var siteCode in siteCodes)
            {
                var clients = Builder<HTSClientExtract>.CreateListOfSize(count).All().With(x => x.SiteCode = siteCode).Build()
                    .ToList();
                var linkages = Builder<HTSClientLinkageExtract>.CreateListOfSize(count).All().With(x => x.SiteCode = siteCode).Build()
                    .ToList();
                var partners = Builder<HTSClientPartnerExtract>.CreateListOfSize(count).All().With(x => x.SiteCode = siteCode).Build()
                    .ToList();

                list.Add(new HtsMessage(clients));
                list.Add(new HtsMessage(linkages));
                list.Add(new HtsMessage(partners));
            }
            return list;
        }



        public static ArtMessageBag ArtMessageBag(int count, int siteCode)
        {
            var list = Builder<ArtMessageBag>.CreateNew().Build();
            list.Message = ArtMessages(count, siteCode);
            return list;
        }

        private static ArtMessage ArtMessages(int count, int siteCode)
        {
            var patientExtractView =
                Builder<PatientExtractView>.CreateNew().With(x => x.SiteCode = siteCode).Build();

            var masterPatientIndices = Builder<PatientArtExtractView>.CreateListOfSize(count).All()
                .With(x => x.SiteCode = siteCode).Build()
                .ToList();
            patientExtractView.PatientArtExtracts = masterPatientIndices;
            return new ArtMessage(patientExtractView);
        }
    }
}
