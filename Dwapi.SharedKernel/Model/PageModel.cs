using System;
using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class PageModel
    {
        public int? Page { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public int SortOrder { get; set; }
    }
}
