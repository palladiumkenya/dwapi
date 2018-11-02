using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Exchange
{
    public class DwhManifest
    {
        public int SiteCode { get; set; }
        public List<int> PatientPks { get; set; } =new List<int>();

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
            var getPks = profiles.ToList();
            var list=new List<DwhManifest>();

            if(getPks.Any())
                list.Add(new DwhManifest(getPks.First().SiteCode,getPks.Select(x=>x.PatientPk).ToList()));

            return list;
        }
    }
}