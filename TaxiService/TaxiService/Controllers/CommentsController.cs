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
    public class CommentsController : ApiController
    {
        private IUnitOfWork unitOfWork;

        // GET: api/Comments
        public IEnumerable<Comment> GetComments()
        {
            return unitOfWork.CommentRepository.GetAll();
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult GetComment(int id)
        {
            Comment comment = unitOfWork.CommentRepository.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            try
            {
                unitOfWork.CommentRepository.Update(comment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public IHttpActionResult PostComment(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.CommentRepository.Add(comment);
            unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public IHttpActionResult DeleteComment(int id)
        {
            Comment comment = unitOfWork.CommentRepository.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            unitOfWork.CommentRepository.Remove(comment);
            unitOfWork.Commit();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            Comment comment = unitOfWork.CommentRepository.GetById(id);

            if(comment!=null)
            {
                return true;
            }

            return false;
        }
    }
}