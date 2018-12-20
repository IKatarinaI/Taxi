using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TaxiService.Models;
using TaxiService.Persistance;
using TaxiService.Persistance.Interface;

namespace TaxiService.Controllers
{
    public class CarsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Cars
        public IEnumerable<Car> GetCars()
        {
            return unitOfWork.CarRepository.GetAll();
        }

        // GET: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = unitOfWork.CarRepository.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.CarRepository.Update(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.CarRepository.Add(car);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = unitOfWork.CarRepository.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            unitOfWork.CarRepository.Remove(car);
            unitOfWork.Commit();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            Car car = unitOfWork.CarRepository.GetById(id);

            if(car!=null)
            {
                return true;
            }

            return false;
        }
    }
}