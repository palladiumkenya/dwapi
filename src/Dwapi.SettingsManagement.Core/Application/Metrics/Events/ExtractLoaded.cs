using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SettingsManagement.Core.DTOs;
using Dwapi.SharedKernel.Enum;
using MediatR;

namespace Dwapi.SettingsManagement.Core.Application.Metrics.Events
{
    public class ExtractLoaded : INotification
    {
        public string Name { get; }
        public int NoLoaded { get; }
        public string Version { get; }
        public DateTime ActionDate { get; } = DateTime.Now;
        public EmrSetup EmrSetup { get;  }
        public List<ExtractCargoDto> ExtractCargos { get; private set; } = new List<ExtractCargoDto>();

        public ExtractLoaded(string name, string version, EmrSetup emrSetup = EmrSetup.SingleFacility)
        {
            Name = name;
            Version = version;
            EmrSetup = emrSetup;
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
            if (Name == "PREP")
                cargos = cargoDtos.Where(x => x.DocketId == "PREP").ToList();

            ExtractCargos = cargos;
        }
    }
}
