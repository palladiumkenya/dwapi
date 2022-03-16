using System;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class AppFeature
    {
        public Feature PKV { get;  }
        public FeatureBatchSize BatchSize { get;  }
        private AppFeature()
        {
        }
        public AppFeature(Feature pkv, FeatureBatchSize batchSize)
        {
            PKV = pkv;
            BatchSize = batchSize;
        }

        public static AppFeature Load(string name, string description, string key,int pats,int visits,int extracts,bool isDevMode = false)
        {
            return new AppFeature(
                new Feature(name, description, key, isDevMode),
                new FeatureBatchSize(pats,visits,extracts)
            );
        }
    }

    public class Feature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public DateTime Expiry { get; set; } = new DateTime(2020, 8, 1);

        public bool IsValid => DateTime.Now.Date < Expiry.Date && ValidateKey();

        public bool IsDevMode { get; }

        private Feature()
        {

        }
        public Feature(string name, string description, string key,bool isDevMode)
        {
            Name = name;
            Description = description;
            Key = key;
            IsDevMode = isDevMode;
        }

        private bool ValidateKey()
        {
            if (IsDevMode)
                return true;

            if (Name.IsSameAs("PKV Decode"))
                return Key.IsSameAs("MAUN-ECC6-11EA");

            return false;
        }

        public override string ToString()
        {
            return $"{Description} | Enabled:{IsValid}";
        }
    }
    public class FeatureBatchSize
    {
        public int Patients { get; set; }
        public int Visits { get; set; }
        public int Extracts { get; set; }


        public FeatureBatchSize(int patients, int visits, int extracts)
        {
            Patients = patients > 2000 ? 2000 : patients;
            Visits = visits > 2000 ? 2000 : visits;
            Extracts = extracts > 2000 ? 2000 : extracts;
        }

        public override string ToString()
        {
            return $"Upload Batch Patients:{Patients} | Visits:{Visits} |Extracts:{Extracts}";
        }
    }
}
