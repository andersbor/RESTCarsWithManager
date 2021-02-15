using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CarsWithManager.Managers;
using CarsWithManager.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace CarsWithManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsManager _manager = new CarsManager();

        // GET: api/<CarsController>
        [HttpGet]
        [ProducesResponseType(Status200OK)]
        public List<Car> Get([FromQuery] string vendor, [FromQuery] string minPrice, [FromQuery] string maxPrice)
        {
            int? minPriceInt = Convert(minPrice); // int? = int + null
            int? maxPriceInt = Convert(maxPrice);
            List<Car> cars = _manager.GetAll(vendor, minPriceInt, maxPriceInt);
            return cars;
        }

        private int? Convert(string str)
        {
            if (str == null) return null;
            try { return int.Parse(str); }
            catch (FormatException) { return null; }
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Car> Get(int id)
        {
            Car car = _manager.GetById(id);
            if (car == null) return NotFound("No such id: " + id);
            return car;
        }

        // POST api/<CarsController>
        [HttpPost]
        [ProducesResponseType(Status201Created)]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            Car newCar = _manager.Add(car);
            string uri = Url.RouteUrl(RouteData.Values) + "/" + newCar.Id;
            return Created(uri, car);
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Car> Put(int id, [FromBody] Car car)
        {
            Car updatedCar = _manager.Update(id, car);
            if (updatedCar == null) return NotFound(id);
            return Ok(updatedCar);
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Car> Delete(int id)
        {
            Car deletedCar = _manager.Delete(id);
            if (deletedCar == null) return NotFound(id);
            return Ok(deletedCar);
        }
    }
}
