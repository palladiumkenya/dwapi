using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;
using Newtonsoft.Json;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifest
    {
        public Guid Id { get; private set; }
        public int SiteCode { get; set; }
        public List<int> PatientPks { get; set; } = new List<int>();
        public string Metrics { get; set; }

        public string Name { get; set; }
        public Guid EmrId { get; set; }
        public string EmrName { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public List<FacMetric> FacMetrics { get; set; } = new List<FacMetric>();
        public UploadMode UploadMode { get; set; }

        public Guid Session { get; set; }
        public DateTime Start { get; set; }
        public string Tag { get; set; }

        public DwhManifest()
        {
        }

        public DwhManifest(int siteCode)
        {
            Id = LiveGuid.NewGuid();
            SiteCode = siteCode;
        }

        public DwhManifest(int siteCode, List<int> patientPks,string siteName,EmrDto emrDto)
        {
            Id = LiveGuid.NewGuid();
            SiteCode = siteCode;
            PatientPks = patientPks;
            Name = siteName;
            EmrId = emrDto.EmrId;
            EmrName = emrDto.Name;
            EmrSetup = emrDto.EmrSetup;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientPks.Count}";
        }

        public static IEnumerable<DwhManifest> Create(IEnumerable<SitePatientProfile> profiles, EmrDto emrDto,
            IEnumerable<Site> sites)
        {
            var list = new List<DwhManifest>();

            if (emrDto.EmrSetup == EmrSetup.SingleFacility)
            {
                var site = sites.OrderByDescending(x => x.PatientCount).First();
                var manifest = new DwhManifest(site.SiteCode, profiles.Select(x => x.PatientPk).ToList(),site.SiteName,emrDto);
                list.Add(manifest);
                return list;
            }

            // multi site setup

            foreach (var site in sites)
            {
                var pks = profiles
                    .Where(x => x.SiteCode == site.SiteCode)
                    .Select(x => x.PatientPk)
                    .ToList();
                var manifest = new DwhManifest(site.SiteCode, pks,site.SiteName,emrDto);
                list.Add(manifest);
            }

            return list.OrderByDescending(x=>x.PatientPks.Count);
        }

        public void AddCargo(Metric metric)
        {
            if (null == metric)
                return;
            var items = JsonConvert.SerializeObject(metric);
            Metrics = items;
        }

        public void AddCargo(CargoType cargoType, object metric)
        {
            if (null == metric)
                return;

            var items = JsonConvert.SerializeObject(metric);

            FacMetrics.Add(new FacMetric(cargoType,items));
        }

        public void InitSession(Guid session,DateTime start, string tag)
        {
            Session = session;
            Start = start;
            Tag = tag;
        }

        public void GenerateID()
        {
            Id =LiveGuid.NewGuid();
        }
    }
}
