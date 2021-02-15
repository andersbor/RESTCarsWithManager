using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarsWithManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarsWithManager.Models;

namespace CarsWithManager.Managers.Tests
{
    [TestClass()]
    public class CarsManagerTests
    {
        private readonly CarsManager _manager = new CarsManager();
        [TestMethod]
        public void TestItAll()
        {
            List<Car> cars = _manager.GetAll();
            Assert.AreEqual(3, cars.Count);

            cars = _manager.GetAll(vendor: "V");
            Assert.AreEqual(1, cars.Count);

            cars = _manager.GetAll(minPrice: 20);
            Assert.AreEqual(2, cars.Count);

            cars = _manager.GetAll(minPrice: 21);
            Assert.AreEqual(1, cars.Count);

            cars = _manager.GetAll(maxPrice: 20);
            Assert.AreEqual(2, cars.Count);

            cars = _manager.GetAll(maxPrice: 18);
            Assert.AreEqual(1, cars.Count);
     
            Car car = _manager.GetById(1);
            Assert.AreEqual(1, car.Id);

            Assert.IsNull(_manager.GetById(100));

            int howMany = _manager.GetAll().Count;
            Car newCar = new Car {Vendor = "WV", Model = "Polo", Price=25};
            Car c = _manager.Add(newCar);
            Assert.AreEqual(howMany +1, _manager.GetAll().Count);

            Car updatedCar = _manager.Update(c.Id, new Car() {Vendor = "WV", Model = "Polo", Price = 26});
            Assert.AreEqual(26, updatedCar.Price);

            Assert.IsNull(_manager.Update(100, null));

            howMany = _manager.GetAll().Count;
            Car deletedCar = _manager.Delete(1);
            Assert.AreEqual(1, deletedCar.Id);

            Assert.IsNull(_manager.Delete(100));
        }
    }
}