using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.SharedKernel.Enum;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class AppMetric : Entity<Guid>
    {
        public string Version { get; set; }
        public string Name { get; set; }
        public DateTime LogDate { get; set; } = DateTime.Now;
        public string LogValue { get; set; }
        public MetricStatus Status { get; set; }
        [NotMapped] public string Display => GetName();
        [NotMapped] public string Action => GetAction();
        [NotMapped] public int Rank => GetRank();



        public AppMetric()
        {
        }

        public AppMetric(string version, string name, string logValue)
        {
            Version = version;
            Name = name;
            LogValue = logValue;
        }

        private string GetName()
        {
            if (Name == "CareTreatment")
                return "Care and Treatment";

            if (Name == "HivTestingService")
                return "Hiv Testing Services";

            if (Name == "MasterPatientIndex")
                return "Master Patient Index";

            if (Name == "MigrationService")
                return "Migration Services";

            return "Other";
        }

        private string GetAction()
        {
            if (LogValue.Contains("NoLoaded"))
                return "Loaded";

            if (LogValue.Contains("NoSent"))
                return "Sent";

            return string.Empty;
        }

        private int GetRank()
        {
            if (LogValue.Contains("NoLoaded") && Name == "CareTreatment")
                return 1;

            if (LogValue.Contains("NoSent") && Name == "CareTreatment")
                return 2;

            if (LogValue.Contains("NoLoaded") && Name == "HivTestingService")
                return 3;

            if (LogValue.Contains("NoSent") && Name == "HivTestingService")
                return 4;

            if (LogValue.Contains("NoLoaded") && Name == "MasterPatientIndex")
                return 5;

            if (LogValue.Contains("NoSent") && Name == "MasterPatientIndex")
                return 6;

            if (LogValue.Contains("NoLoaded") && Name == "MigrationService")
                return 7;

            if (LogValue.Contains("NoSent") && Name == "MigrationService")
                return 8;

            return 99;
        }

        public void CreatCt(string logValue)
        {
            Name = "CareTreatment";
            LogValue =logValue;
            LogDate=new DateTime(1983,4,7);
        }

        public void CreatHts(string logValue)
        {
            Name = "HivTestingService";
            LogValue =logValue;
            LogDate=new DateTime(1983,4,7);
        }

        public void CreatMpi(string logValue)
        {
            Name = "MasterPatientIndex";
            LogValue =logValue;
            LogDate=new DateTime(1983,4,7);
        }

        public void CreatMgs(string logValue)
        {
            Name = "MigrationService";
            LogValue =logValue;
            LogDate=new DateTime(1983,4,7);
        }
    }
}
