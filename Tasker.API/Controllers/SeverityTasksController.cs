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
        private TasksDBContext db = new TasksDBContext();

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
        public IHttpActionResult PostSeverityTask(Task task, SeverityTask severityTask)
        {
            if (!ModelState.IsValid || task.Id != severityTask.TaskId || TaskExists(task.Id))
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.SeverityTasks.Add(severityTask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = severityTask.TaskId }, severityTask);
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

        private bool TaskExists(int id)
        {
            return db.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}