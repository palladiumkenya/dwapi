using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.Domain.Utils;

namespace Dwapi.ExtractsManagement.Core.Model.Source.Dwh
{
    public abstract class TempExtract
    {
        [Key]
        //[DoNotRead]
        public Guid Id { get; set; }
        public int? PatientPK { get; set; }
        public string PatientID { get; set; }
        public int? FacilityId { get; set; }
        public int? SiteCode { get; set; }

        //[DoNotRead]
        public DateTime DateExtracted { get; set; }
        //[DoNotRead]
        public bool CheckError { get; set; }

        //[DoNotRead]
        [NotMapped]
        public bool HasError { get; set; }

        protected TempExtract()
        {
            Id = LiveGuid.NewGuid();
            DateExtracted = DateTime.Now;
        }

        ////TODO Create default converter in Shared Project
        //public virtual void Load(IDataReader reader)
        //{
        //    HasError = false;
        //    foreach (var p in GetType().GetProperties())
        //    {
        //        if (!Attribute.IsDefined(p, typeof(DoNotReadAttribute)))
        //            p.SetValue(this, reader.Get(p.Name, p.PropertyType));
        //    }
        //    HasError = !IsValid();
        //}

        //public void Load(IDataReader reader, PropertyInfo[] propertyInfos)
        //{
        //    HasError = false;
        //    foreach (var p in propertyInfos)
        //    {
        //        if (!Attribute.IsDefined(p, typeof(DoNotReadAttribute)))
        //            p.SetValue(this, reader.Get(p.Name, p.PropertyType));
        //    }
        //    HasError = !IsValid();
        //}

        //public Task LoadAsync(IDataReader reader)
        //{
        //    var t = Task.Run(() =>
        //    {
        //        Load(reader);
        //    });
        //    return t;
        //}

        //public virtual bool IsValid()
        //{
        //    //TODO:define the validity of record
        //    return PatientPK > 0 &&
        //           SiteCode > 0 &&
        //           !string.IsNullOrWhiteSpace(PatientID);
        //}

        //public virtual string GetAddAction()
        //{
        //    StringBuilder scb = new StringBuilder();
        //    List<string> columns = new List<string>();
        //    foreach (var p in GetType().GetProperties())
        //    {
        //        if (!Attribute.IsDefined(p, typeof(NotMappedAttribute)))
        //            columns.Add(p.Name);
        //    }

        //    if (columns.Count > 1)
        //    {
        //        scb.Append($" INSERT INTO {GetType().Name} ");
        //        scb.Append($" ({Utility.GetColumns(columns)}) ");
        //        scb.Append($" VALUES({Utility.GetParameters(columns)}) ");
        //    }

        //    return scb.ToString();
        //}

        //public virtual string GetAddErrorAction()
        //{
        //    StringBuilder scb = new StringBuilder();
        //    List<string> columns = new List<string>();
        //    foreach (var p in GetType().GetProperties())
        //    {
        //        if (!Attribute.IsDefined(p, typeof(NotMappedAttribute)))
        //            columns.Add(p.Name);
        //    }

        //    if (columns.Count > 1)
        //    {
        //        scb.Append($" INSERT INTO {GetType().Name}Error ");
        //        scb.Append($" ({Utility.GetColumns(columns)}) ");
        //        scb.Append($" VALUES({Utility.GetParameters(columns)}) ");
        //    }

        //    return scb.ToString();
        //}

        //public void Load(IDataReader reader, TypeAccessor typeAccessor, List<Member> fmembers, List<ExMap> exMaps)
        //{
        //    foreach (var p in fmembers)
        //    {

        //        var ordinal = GetOrdinal(p.Name, exMaps);
        //        if (ordinal > -1)
        //            typeAccessor[this, p.Name] = reader.Get(p.Name, p.Type, ordinal);
        //    }
        //}

        //private int GetOrdinal(string name, List<ExMap> exMaps)
        //{
        //    var ordinal = exMaps.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
        //    return ordinal?.Ordinal ?? -1;
        //}
    }
}
