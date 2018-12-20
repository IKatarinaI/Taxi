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
    public class AddressesController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Addresses
        public IEnumerable<Address> GetAddresses()
        {
            return unitOfWork.AddressRepository.GetAll();
        }

        // GET: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult GetAddress(int id)
        {
            Address address = unitOfWork.AddressRepository.GetById(id);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddress(int id, Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != address.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.AddressRepository.Update(address);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [ResponseType(typeof(Address))]
        public IHttpActionResult PostAddress(Address address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.AddressRepository.Add(address);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [ResponseType(typeof(Address))]
        public IHttpActionResult DeleteAddress(int id)
        {
            Address address = unitOfWork.AddressRepository.GetById(id);

            if (address == null)
            {
                return NotFound();
            }

            unitOfWork.AddressRepository.Remove(address);
            unitOfWork.Commit();

            return Ok(address);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddressExists(int id)
        {
            Address address = unitOfWork.AddressRepository.GetById(id);

            if(address!=null)
            {
                return true;
            }

            return false;
        }
    }
}