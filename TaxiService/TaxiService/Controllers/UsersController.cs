using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using TaxiService.Models;
using TaxiService.Persistance.Interface;

namespace TaxiService.Controllers
{
    public class UsersController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return unitOfWork.UserRepository.GetAll();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = unitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.UserRepository.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.UserRepository.Add(user);

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = unitOfWork.UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            unitOfWork.UserRepository.Remove(user);
            unitOfWork.Commit();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            User user = unitOfWork.UserRepository.GetById(id);

            if(user!=null)
            {
                return true;
            }

            return false;
        }
    }
}