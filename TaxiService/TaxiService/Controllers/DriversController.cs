using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TaxiService.Models;
using TaxiService.Persistance.Interface;

namespace TaxiService.Controllers
{
    public class DriversController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Drivers
        public IEnumerable<Driver> GetDrivers()
        {
            return unitOfWork.DriverRepository.GetAll();
        }

        // GET: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public IHttpActionResult GetDriver(int id)
        {
            Driver driver = unitOfWork.DriverRepository.GetById(id);

            if (driver == null)
            {
                return NotFound();
            }

            return Ok(driver);
        }

        // PUT: api/Drivers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDriver(int id, Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != driver.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.DriverRepository.Update(driver);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        // POST: api/Drivers
        [ResponseType(typeof(Driver))]
        public IHttpActionResult PostDriver(Driver driver)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.DriverRepository.Add(driver);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = driver.Id }, driver);
        }

        // DELETE: api/Drivers/5
        [ResponseType(typeof(Driver))]
        public IHttpActionResult DeleteDriver(int id)
        {
            Driver driver = unitOfWork.DriverRepository.GetById(id);

            if (driver == null)
            {
                return NotFound();
            }

            unitOfWork.DriverRepository.Remove(driver);
            unitOfWork.Commit();

            return Ok(driver);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DriverExists(int id)
        {
            Driver driver = unitOfWork.DriverRepository.GetById(id);

            if(driver!=null)
            {
                return true;
            }

            return false;
        }
    }
}