using Dwapi.SharedKernel.Model;

namespace Dwapi.SettingsManagement.Core.Model
{
    public class IndicatorKey : Entity<string>
    {
        public string Description { get; set; }
        public decimal Rank { get; set; }
    }
}
