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
using Tasker.API.Models;

namespace Tasker.API.Controllers
{
    public class SeverityTasksController : ApiController
    {
        private TasksContext db = new TasksContext();

        // GET: api/Tasks
        //public IQueryable<Task> GetTasks()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    return db.Tasks;
        //}

        // GET: api/SeverityTasks/5
        [ResponseType(typeof(SeverityTask))]
        public IHttpActionResult GetSeverityTask(int id)
        {
            SeverityTask severityTask= db.SeverityTasks.Find(id);
            if (severityTask == null)
            {
                return NotFound();
            }

            return Ok(severityTask);
        }

        // PUT: api/SeverityTasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeverityTask(int id, SeverityTask severityTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != severityTask.TaskId)
            {
                return BadRequest();
            }

            db.Entry(severityTask).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeverityTaskExists(id))
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

        // POST: api/SeverityTasks
        [ResponseType(typeof(SeverityTask))]
        public IHttpActionResult PostSeverityTask(SeverityTask severityTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SeverityTasks.Add(severityTask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = severityTask.TaskId }, severityTask);
        }

        // DELETE: api/SeverityTasks/5
        [ResponseType(typeof(SeverityTask))]
        public IHttpActionResult DeleteSeverityTask(int id)
        {
            SeverityTask severityTask = db.SeverityTasks.Find(id);
            if (severityTask == null)
            {
                return NotFound();
            }

            db.SeverityTasks.Remove(severityTask);
            db.SaveChanges();

            return Ok(severityTask);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SeverityTaskExists(int id)
        {
            return db.SeverityTasks.Count(e => e.TaskId == id) > 0;
        }
    }
}