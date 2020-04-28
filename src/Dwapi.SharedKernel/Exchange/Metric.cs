using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Exchange
{
    public class Metric:Entity<Guid>
    {
        public string EmrName { get; set; }
        public string EmrVersion { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastMoH731RunDate { get; set; }
        public DateTime DateExtracted { get; set; }
    }
}
