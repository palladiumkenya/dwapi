using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Dwapi.Domain.Utils;

namespace Dwapi.Domain.Abstract
{
    public abstract class ClientExtract
    {
        //private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public virtual Guid Id { get; set; }
        public virtual int PatientPK { get; set; }
        public string PatientID { get; set; }
        public virtual int SiteCode { get; set; }
        [Column(Order = 100)]
        public virtual string Emr { get; set; }
        [Column(Order = 101)]
        public virtual string Project { get; set; }
        [DoNotRead]
        [Column(Order = 102)]
        public virtual bool? Processed { get; set; }
        [DoNotRead]
        public virtual string QueueId { get; set; }
        [DoNotRead]
        public virtual string Status { get; set; }
        [DoNotRead]
        public virtual DateTime? StatusDate { get; set; }

        protected ClientExtract()
        {
            Id = LiveGuid.NewGuid();
        }

        public bool IsSent()
        {
            return Status.Equals("Sent");
        }

        public virtual string GetAddAction(string source, bool lookup = true)
        {
            StringBuilder scb = new StringBuilder();
            List<string> columns = new List<string>();
            foreach (var p in GetType().GetProperties())
            {
                if (
                    !(Attribute.IsDefined(p, typeof(NotMappedAttribute)) ||
                      p.GetCustomAttributes(typeof(DoNotReadAttribute), false).Length > 0))
                    columns.Add(p.Name);
            }

            if (columns.Count > 1)
            {
                string destination = GetType().Name.Replace("Client", "");

                scb.Append($" INSERT INTO {destination} "); //ART
                if (lookup)
                {
                    scb.Append($" ({Utility.GetColumns(columns)}) ");
                    scb.Append($" SELECT {Utility.GetColumns(columns, "s")} FROM {source} s"); //TEMPART
                    scb.Append($" INNER JOIN PatientExtract p ON ");
                    scb.Append($" s.PatientPK = p.PatientPK AND ");
                    scb.Append($" s.SiteCode = p.SiteCode ");
                    scb.Append($" WHERE s.CheckError = 0");

                }
                else
                {
                    scb.Append($" ({Utility.GetColumns(columns)}) ");
                    scb.Append($" SELECT {Utility.GetColumns(columns)} FROM {source} "); //TEMPART
                }
            }




            return scb.ToString();
        }
    }
}
