using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Model;
using Newtonsoft.Json;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifest
    {
        public int SiteCode { get; set; }
        public List<int> PatientPks { get; set; } =new List<int>();
        public string Metrics { get; set; }

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
            var getPks = profiles.ToList().GroupBy(x => x.SiteCode).ToList();
            var list = new List<DwhManifest>();

            foreach (var pk in getPks)
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
    }
}
