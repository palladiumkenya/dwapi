using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dwapi.ExtractsManagement.Core.Model.Destination.Cbs;
using Dwapi.SharedKernel.Utility;

namespace Dwapi.UploadManagement.Core.Model.Cbs
{
    [Table("MasterPatientIndices")]
    public class MasterPatientIndexView: MasterPatientIndex
    {
    }
}
