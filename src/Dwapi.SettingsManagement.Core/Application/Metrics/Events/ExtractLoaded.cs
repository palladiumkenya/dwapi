using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.DTOs;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events
{
    public class ExtractLoaded : INotification
    {
        public string Name { get; }
        public int NoLoaded { get; }
        public string Version { get; }
        public DateTime ActionDate { get; } = DateTime.Now;
        public List<ExtractCargoDto> ExtractCargos { get; private set; } = new List<ExtractCargoDto>();

        public ExtractLoaded(string name,  string version)
        {
            Name = name;
            Version = version;
        }

        public ExtractLoaded(string name, int noLoaded, string version)
        {
            Name = name;
            NoLoaded = noLoaded;
            Version = version;
        }

        public void AddCargo(List<ExtractCargoDto> cargoDtos)
        {
            var cargos = new List<ExtractCargoDto>();
            if (Name == "CareTreatment")
                cargos = cargoDtos.Where(x => x.DocketId == "NDWH").ToList();
            if (Name == "HivTestingService")
                cargos = cargoDtos.Where(x => x.DocketId == "HTS").ToList();
            if (Name == "MasterPatientIndex")
                cargos = cargoDtos.Where(x => x.DocketId == "CBS").ToList();
            if (Name == "MNCH")
                cargos = cargoDtos.Where(x => x.DocketId == "MNCH").ToList();

            ExtractCargos = cargos;
        }
    }
}
