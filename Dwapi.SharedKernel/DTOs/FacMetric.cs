using Dwapi.SharedKernel.Enum;

namespace Dwapi.SharedKernel.DTOs
{
    public class FacMetric
    {
        public CargoType CargoType { get; }
        public string Metric { get; }

        public FacMetric()
        {

        }
        public FacMetric(CargoType cargoType, string metric)
        {
            CargoType = cargoType;
            Metric = metric;
        }
    }
}
