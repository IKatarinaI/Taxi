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
    public class LocationsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Locations
        public IEnumerable<Location> GetLocations()
        {
            return unitOfWork.LocationRepository.GetAll();
        }

        // GET: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult GetLocation(int id)
        {
            Location location = unitOfWork.LocationRepository.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.LocationRepository.Update(location);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        [ResponseType(typeof(Location))]
        public IHttpActionResult PostLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.LocationRepository.Add(location);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [ResponseType(typeof(Location))]
        public IHttpActionResult DeleteLocation(int id)
        {
            Location location = unitOfWork.LocationRepository.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            unitOfWork.LocationRepository.Remove(location);
            unitOfWork.Commit();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LocationExists(int id)
        {
            Location location = unitOfWork.LocationRepository.GetById(id);

            if(location!=null)
            {
                return true;
            }

            return false;
        }
    }
}