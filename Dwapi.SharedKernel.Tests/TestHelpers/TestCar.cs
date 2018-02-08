using System;
using Dwapi.SharedKernel.Model;

namespace Dwapi.SharedKernel.Tests.TestHelpers
{
    public class TestCar : Entity<Guid>
    {
        public string Name { get; set; }

        public TestCar()
        {
        }

        public TestCar(string name)
        {
            Name = name;
        }
        
        public override string ToString()
        {
            return $"{Id}|{Name}";
        }
    }
}
