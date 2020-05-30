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
    public class Manifest
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public Guid EmrId { get; set; }
        public string EmrName { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public Guid Id { get; set; } = LiveGuid.NewGuid();
        public DateTime DateLogged { get; set; } = DateTime.Now;
        public List<Cargo> Cargoes { get; set; } = new List<Cargo>();


        public Manifest()
        {
        }

        public Manifest(int siteCode)
        {
            SiteCode = siteCode;
        }

        private Manifest(Site site,EmrDto emrDto)
        {
            SiteCode = site.SiteCode;
            Name = site.SiteName;
            EmrId = emrDto.EmrId;
            EmrName = emrDto.Name;
            EmrSetup = emrDto.EmrSetup;
        }

        public static IEnumerable<Manifest> Create(IEnumerable<SitePatientProfile> profiles, EmrDto emrDto,
            IEnumerable<Site> sites, CargoType type = CargoType.Patient)
        {
            var manifests = new List<Manifest>();

            if (emrDto.EmrSetup == EmrSetup.SingleFacility)
            {
                var site = sites.OrderByDescending(x => x.PatientCount).First();
                var manifest = new Manifest(site,emrDto);
                manifest.AddCargo(profiles.Select(x => x.PatientPk).ToList());
                manifests.Add(manifest);
                return manifests;
            }

            // multi site setup

            foreach (var site in sites)
            {
                var manifest=new Manifest(site,emrDto);
                var pks = profiles
                    .Where(x => x.SiteCode == site.SiteCode)
                    .Select(x => x.PatientPk)
                    .ToList();
                manifest.AddCargo(pks);
                manifests.Add(manifest);
            }
            return manifests;
        }

        public static IEnumerable<Manifest> Create(IEnumerable<SiteMetricProfile> profiles, EmrDto emrDto,
            IEnumerable<Site> sites, CargoType type = CargoType.Patient)
        {
            var manifests = new List<Manifest>();

            if (emrDto.EmrSetup == EmrSetup.SingleFacility)
            {
                var site = sites.OrderByDescending(x => x.PatientCount).First();
                var manifest = new Manifest(site,emrDto);
                manifest.AddCargo(profiles.Select(x => x.MetricId).ToList(), CargoType.Migration);
                manifests.Add(manifest);
                return manifests;
            }

            // multi site setup

            foreach (var site in sites)
            {
                var manifest=new Manifest(site,emrDto);
                var pks = profiles
                    .Where(x => x.SiteCode == site.SiteCode)
                    .Select(x => x.MetricId)
                    .ToList();
                manifest.AddCargo(pks,CargoType.Migration);
                manifests.Add(manifest);
            }
            return manifests;
        }

        public void AddCargo(List<int> patienPks, CargoType type = CargoType.Patient)
        {
            var items = string.Join(',', patienPks);
            Cargoes.Add(new Cargo(type, items, Id));
        }

        public void AddCargo(Metric metric, CargoType type = CargoType.Metrics)
        {
            if(null==metric)
                return;
            var items = JsonConvert.SerializeObject(metric);
            Cargoes.Add(new Cargo(type, items, Id));
        }

        public void AddAppToCargo<T>(List<T> metrics)
        {
            if(null==metrics)
                return;

            if (metrics.Any())
            {
                foreach (var metric  in metrics)
                {
                    var items = JsonConvert.SerializeObject(metric);
                    Cargoes.Add(new Cargo(CargoType.AppMetrics, items, Id));
                }
            }
        }

        public override string ToString()
        {
            return $"{SiteCode}-{Name}";
        }
    }
}
