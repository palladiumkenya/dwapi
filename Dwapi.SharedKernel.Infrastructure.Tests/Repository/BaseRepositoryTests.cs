using System;
using System.Collections.Generic;
using System.Linq;
using Dwapi.SharedKernel.Infrastructure.Tests.TestHelpers;
using Dwapi.SharedKernel.Tests.TestHelpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Dwapi.SharedKernel.Infrastructure.Tests.Repository
{
    [TestFixture]
    public class BaseRepositoryTests
    {
        private TestDbContext _context;
        private TestCarRepository _testCarRepository;
        private List<TestCar> _cars;
        private DbContextOptions<TestDbContext> _options;

        [OneTimeSetUp]
        public void Init()
        {
            Factory.Init();
            _options = TestDbOptions.GetInMemoryOptions<TestDbContext>();
            var context=new TestDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            _cars = Factory.TestCars();
            context.AddRange(_cars);
            context.SaveChanges();
        }

        [SetUp]
        public void SetUp()
        {
            _context = new TestDbContext(_options);
            _testCarRepository = new TestCarRepository(_context);
        }


        [Test]
        public void should_Get_All()
        {
            var cars = _testCarRepository.GetAll().ToList();
            Assert.True(cars.Count > 0);
            foreach (var personMatch in cars)
            {
                Console.WriteLine(personMatch);
            }
        }
    }
}