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
using Tasker.API.Context;

namespace Tasker.API.Controllers
{
    public class TimeTasksController : ApiController
    {
        private TasksDBContext db = new TasksDBContext();

        // GET: api/TimeTasks/5
        [ResponseType(typeof(TimeTask))]
        public IHttpActionResult GetTimeTask(int id)
        {
            TimeTask timeTask = db.TimeTasks.Find(id);
            if (timeTask == null)
            {
                return NotFound();
            }

            return Ok(timeTask);
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
                if (!db.SeverityTaskExists(id))
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
            if (!ModelState.IsValid || task.Id != severityTask.TaskId || db.TaskExists(task.Id))
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
    }
}