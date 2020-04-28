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
        }

        [SetUp]
        public void SetUp()
        {
            var context = new TestDbContext(_options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            _cars = Factory.TestCars();
            context.AddRange(_cars);
            context.SaveChanges();
            _context = new TestDbContext(_options);
            _testCarRepository = new TestCarRepository(_context);
        }

        [Test]
        public void should_Get_By_Id()
        {
            var car = _testCarRepository.Get(_cars.First().Id);
            Assert.NotNull(car);
            Console.WriteLine(car);
        }

        [Test]
        public void should_Create_New()
        {
            var newCar = new TestCar("Velar");
            _testCarRepository.Create(newCar);
            _testCarRepository.SaveChanges();

            var car = _testCarRepository.Get(newCar.Id);
            Assert.NotNull(car);
            Console.WriteLine(car);
        }

        [Test]
        public void should_Update_Exisitng()
        {
            var car = _cars.First();
            car.Name = "GLE Benz";
            _testCarRepository.Update(car);
            _testCarRepository.SaveChanges();

            _testCarRepository = new TestCarRepository(_context);

            var updatedCar = _testCarRepository.Get(car.Id);
            Assert.AreEqual("GLE Benz", updatedCar.Name);
            Console.WriteLine(updatedCar);
        }

        [Test]
        public void should_Create_Or_Update_New()
        {
            var newCar = new TestCar("Velar");
            _testCarRepository.CreateOrUpdate(newCar);
            _testCarRepository.SaveChanges();

            var car = _testCarRepository.Get(newCar.Id);
            Assert.NotNull(car);
            Console.WriteLine(car);
        }

        [Test]
        public void should_Create_Or_Update_Exisitng()
        {
            var car = _cars.First();
            car.Name = "GLE Benz";
            _testCarRepository.CreateOrUpdate(car);
            _testCarRepository.SaveChanges();

            _testCarRepository = new TestCarRepository(_context);

            var updatedCar = _testCarRepository.Get(car.Id);
            Assert.AreEqual("GLE Benz", updatedCar.Name);
            Console.WriteLine(updatedCar);
        }

        [Test]
        public void should_Delete_Exisitng()
        {
            var car = _cars.First();
            Assert.NotNull(car);
            _testCarRepository.Delete(car);
            _testCarRepository.SaveChanges();

            _testCarRepository = new TestCarRepository(_context);

            var deletedCar = _testCarRepository.Get(car.Id);
            Assert.IsNull(deletedCar);
        }
        [Test]
        public void should_Delete_Exisitng_By_Id()
        {
            var car = _cars.First();
            _testCarRepository.Delete(car.Id);
            _testCarRepository.SaveChanges();

            _testCarRepository = new TestCarRepository(_context);

            var deletedCar = _testCarRepository.Get(car.Id);
            Assert.IsNull(deletedCar);
        }

        [Test]
        public void should_Get_All()
        {
            var cars = _testCarRepository.GetAll().ToList();
            Assert.True(cars.Count > 0);
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}
