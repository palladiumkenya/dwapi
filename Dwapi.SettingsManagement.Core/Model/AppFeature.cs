using System;
using System.Collections.Generic;
using Dwapi.SharedKernel.Utility;
using Newtonsoft.Json;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class AppFeature
    {
        public Feature PKV { get;  }

        public AppFeature()
        {
        }
        public AppFeature(Feature pkv)
        {
            PKV = pkv;
        }

        public static AppFeature Load(string name, string description, string key, bool isDevMode = false)
        {
            return new AppFeature(new Feature(name, description, key, isDevMode));
        }
    }

    public class Feature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public DateTime Expiry { get; set; } = new DateTime(2020, 6, 1);

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
}
