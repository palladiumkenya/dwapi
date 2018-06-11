using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Model.Enum;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Model
{
    public class Manifest
    {
        public Guid Id { get; set; } = LiveGuid.NewGuid();
        public DateTime DateLogged { get; set; }=DateTime.Now;
        public int SiteCode { get; set; }
        public string SiteName { get; set; }
        public List<Cargo> Cargoes { get; set; } = new List<Cargo>();

        public void AddCargo(List<int> patienPks, CargoType type = CargoType.Patient)
        {
            var items = string.Join(',',patienPks);
            Cargoes.Add(new Cargo(type, items,Id));
        }

        private   Manifest(int siteCode, string siteName)
        {
            SiteCode = siteCode;
            SiteName = siteName;
        }

        public static IEnumerable<Manifest> Create(IEnumerable<SitePatientProfile> profiles, CargoType type = CargoType.Patient)
        {
            var manifests=new List<Manifest>();
            var p = profiles.ToList();
            var m=new Manifest(p.First().SiteCode, p.First().SiteName);
            m.AddCargo(p.Select(x=>x.PatientPk).ToList());
            manifests.Add(m);
            return manifests;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{SiteName}";
        }
    }
}