using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SharedKernel.Exchange
{
    public class Manifest
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; } = LiveGuid.NewGuid();
        public DateTime DateLogged { get; set; } = DateTime.Now;
        public List<Cargo> Cargoes { get; set; } = new List<Cargo>();

        public void AddCargo(List<int> patienPks, CargoType type = CargoType.Patient)
        {
            var items = string.Join(',', patienPks);
            Cargoes.Add(new Cargo(type, items, Id));
        }

        public Manifest()
        {
        }

        public Manifest(int siteCode)
        {
            SiteCode = siteCode;
        }

        private Manifest(int siteCode, string name)
        {
            SiteCode = siteCode;
            Name = name;
        }

        public static IEnumerable<Manifest> Create(IEnumerable<SitePatientProfile> profiles,
            CargoType type = CargoType.Patient)
        {
            var manifests = new List<Manifest>();
            var p = profiles.ToList();
            var m = new Manifest(p.First().SiteCode, p.First().SiteName);
            m.AddCargo(p.Select(x => x.PatientPk).ToList());
            manifests.Add(m);
            return manifests;
        }

        public override string ToString()
        {
            return $"{SiteCode}-{Name}";
        }
    }
}