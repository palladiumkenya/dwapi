using Dwapi.SharedKernel.Enum;

namespace Dwapi.SharedKernel.DTOs
{
    public class FacMetric
    {
        public CargoType CargoType { get; set;}
        public string Metric { get; set; }

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
