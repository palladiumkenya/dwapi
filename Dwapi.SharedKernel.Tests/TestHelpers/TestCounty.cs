using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Tests.TestHelpers
{
    public class TestCounty : Entity<int>
    {
        public string Name { get; set; }

        public TestCounty()
        {
        }

        public TestCounty(int id, string name) : base(id)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}|{Name}";
        }
    }
}
