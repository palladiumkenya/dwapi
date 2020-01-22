using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.DTOs;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Newtonsoft.Json;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifest
    {
        public int SiteCode { get; set; }
        public List<int> PatientPks { get; set; } = new List<int>();
        public string Metrics { get; set; }
        public List<FacMetric> FacMetrics { get; set; } = new List<FacMetric>();

        public DwhManifest()
        {
        }

        public DwhManifest(int siteCode)
        {
            SiteCode = siteCode;
        }

        public DwhManifest(int siteCode, List<int> patientPks)
        {
            SiteCode = siteCode;
            PatientPks = patientPks;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{PatientPks.Count}";
        }

        public static IEnumerable<DwhManifest> Create(IEnumerable<SitePatientProfile> profiles)
        {
            /*
            var getPks = profiles.ToList();
            var list = new List<DwhManifest>();

            if (getPks.Any())
                list.Add(new DwhManifest(getPks.First().SiteCode, getPks.Select(x => x.PatientPk).ToList()));

            return list;*/

            // multi site setup
            
            var groupPKsBySiteCode = profiles.ToList().GroupBy(x => x.SiteCode).ToList();
            var list = new List<DwhManifest>();

            foreach (var pk in groupPKsBySiteCode)
                list.Add(new DwhManifest(pk.First().SiteCode, pk.Select(x => x.PatientPk).ToList()));

            return list;
            
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
    }
}
