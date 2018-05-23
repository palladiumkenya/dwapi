using System;
using System.Collections.Generic;
using System.Text;

namespace Dwapi.SharedKernel.Model
{
    public class DwhProgress
    {
        public string Extract { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }

        public DwhProgress(string extract, string status)
        {
            Extract = extract;
            Status = status;
        }

        public DwhProgress(string extract, string status, int count):this(extract,status)
        {
            Count = count;
        }
    }
}
