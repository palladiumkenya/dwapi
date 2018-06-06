using Dwapi.SharedKernel.Model;
using MediatR;
using System;
using System.Collections.Generic;

namespace Dwapi.ExtractsManagement.Core.Commands
{
    public class LoadFromEmrCommand : IRequest<LoadFromEmrResponse>
    {
        public IList<DwhExtract> Extracts { get; set; }
        //public Guid DbProtocolId { get; set; }
        public DbProtocol DatabaseProtocol { get; set; }

    }

    public class DwhExtract
    {
        public Guid Id { get; set; }
        public string Emr { get; set; }
        public string SqlQuery { get; set; }
        public ExtractType ExtractType => 
            (ExtractType)Enum.Parse(typeof(ExtractType), this.ExtractName.Trim());
        public string ExtractName { get; set; }
        public int Rank => (int)this.ExtractType;
    }

    public enum ExtractType
    {
        Patient,
        PatientArt,
        PatientBaseline,
        PatientLab,
        PatientPharmacy,
        PatientStatus,
        PatientVisit
    }

    public class ExtractTypeMapping
    {
        private IDictionary<ExtractType, Type> _dict
            => new Dictionary<ExtractType, Type>();

        public void RegisterMapping(ExtractType extractType, Type type)
        {
            _dict.Add(extractType, type);
        }

        public Type GetMappingFor(ExtractType extractType)
        {
            if (_dict.TryGetValue(extractType, out Type type))
                return type;
            throw new InvalidOperationException();
        }

    }

    public class LoadFromEmrResponse
    {

    }
}
