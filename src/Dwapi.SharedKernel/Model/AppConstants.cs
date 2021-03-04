using System.Collections.Generic;

namespace Dwapi.SharedKernel.Model
{
    public class AppConstants
    {
        public static bool DiffSupport { get; set; }
        public static bool DiffSupportChecked { get; set; }

        public List<KeyValuePair<string,string>> TableLookup { get; set; }
    }
}
