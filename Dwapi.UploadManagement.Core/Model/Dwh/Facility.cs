using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.UploadManagement.Core.Model.Dwh
{
    public class Facility
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }

        public Facility()
        {
        }

        public Facility(int code, string name, string emr, string project)
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }
    }
}
