using System.Collections.Generic;
using CarsWithManager.Models;

namespace CarsWithManager.Managers
{
    public class CarsManager
    {
        private static int _nextId = 1;

        private static readonly List<Car> Data = new List<Car>
        {
            new Car {Id=_nextId++, Model = "Amazon", Make = "Volvo", Price = 20},
            new Car {Id=_nextId++, Model = "A8", Make = "Audi", Price = 30},
            new Car {Id=_nextId++, Model = "Punto", Make = "Fiat", Price = 10}
        };

        public List<Car> GetAll(string vendor=null, int? minPrice=null, int? maxPrice=null)
        // int? = int + null
        {
            List<Car> cars = new List<Car>(Data);
            if (vendor != null)
                cars = cars.FindAll(car => car.Make.StartsWith(vendor));
            if (minPrice != null)
            {
                cars = cars.FindAll(car => car.Price >= minPrice);
            }
            if (maxPrice != null)
            {
                cars = cars.FindAll(car => car.Price <= maxPrice);
            }
            return cars;
        }

        public Car GetById(int id)
        {
            return Data.Find(car => car.Id == id);
        }

        public Car Add(Car car)
        {
            car.Id = _nextId++;
            Data.Add(car);
            return car;
        }

        public Car Update(int id, Car updates)
        {
            Car car = Data.Find(c => c.Id == id);
            if (car == null) { return null; }
            car.Make = updates.Make;
            car.Model = updates.Model;
            car.Price = updates.Price;
            return car;
        }

        public Car Delete(int id)
        {
            Car car = Data.Find(c => c.Id == id);
            if (car == null) { return null; }
            bool removed = Data.Remove(car); // should return true
            return car;
        }
    }
}
