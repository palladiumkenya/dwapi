using System;
using Dwapi.SharedKernel.Enum;

namespace Dwapi.SharedKernel.DTOs
{
    public class EmrDto
    {
        public Guid EmrId { get; set; }
        public string Name { get; set; }
        public EmrSetup EmrSetup { get; set; }

        public EmrDto(Guid emrId, string name, EmrSetup emrSetup)
        {
            EmrId = emrId;
            Name = name;
            EmrSetup = emrSetup;
        }
    }
}
