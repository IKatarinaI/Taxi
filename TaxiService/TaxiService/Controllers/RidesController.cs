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
    public class RidesController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Rides
        public IEnumerable<Ride> GetRides()
        {
            return unitOfWork.RideRepository.GetAll();
        }

        // GET: api/Rides/5
        [ResponseType(typeof(Ride))]
        public IHttpActionResult GetRide(int id)
        {
            Ride ride = unitOfWork.RideRepository.GetById(id);

            if (ride == null)
            {
                return NotFound();
            }

            return Ok(ride);
        }

        // PUT: api/Rides/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRide(int id, Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ride.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.RideRepository.Update(ride);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RideExists(id))
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

        // POST: api/Rides
        [ResponseType(typeof(Ride))]
        public IHttpActionResult PostRide(Ride ride)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.RideRepository.Add(ride);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = ride.Id }, ride);
        }

        // DELETE: api/Rides/5
        [ResponseType(typeof(Ride))]
        public IHttpActionResult DeleteRide(int id)
        {
            Ride ride = unitOfWork.RideRepository.GetById(id);

            if (ride == null)
            {
                return NotFound();
            }

            unitOfWork.RideRepository.Remove(ride);
            unitOfWork.Commit();

            return Ok(ride);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RideExists(int id)
        {
            Ride ride = unitOfWork.RideRepository.GetById(id);

            if(ride!=null)
            {
                return true;
            }

            return false;
        }
    }
}