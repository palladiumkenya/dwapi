using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dwapi.Domain
{
    [Table("Facility")]
    public class ClientFacility
    {
        [Key]
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Emr { get; set; }
        // todo: ask if necessary
        public string Project { get; set; }

        public ClientFacility()
        {

        }
        public ClientFacility(int code, string name, string emr, string project)
        {
            Code = code;
            Name = name;
            Emr = emr;
            Project = project;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }

        //public string GetAddAction(string source)
        //{
        //    StringBuilder scb = new StringBuilder();
        //    List<string> columns = new List<string>();
        //    foreach (var p in GetType().GetProperties())
        //    {
        //        if (
        //            !(Attribute.IsDefined(p, typeof(NotMappedAttribute)) ||
        //              p.GetCustomAttributes(typeof(DoNotReadAttribute), false).Length > 0))
        //            columns.Add(p.Name);
        //    }

        //    if (columns.Count > 1)
        //    {
        //        string destination = GetType().Name.Replace("Client", "");

        //        scb.Append($" INSERT INTO {destination} "); //ART
        //        scb.Append($" ({Utility.GetColumns(columns)}) ");
        //        scb.Append($" SELECT  FROM {source}"); //TEMPART

        //    }
        //    return scb.ToString();
        //}
    }
}
