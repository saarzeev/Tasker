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

        // PUT: api/TimeTasks/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimeTasks(int id, TimeTask timeTasks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeTasks.TaskId)
            {
                return BadRequest();
            }

            db.Entry(timeTasks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.TimeTaskExists(id))
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

        // POST: api/TimeTasks
        [ResponseType(typeof(TimeTask))]
        public IHttpActionResult PostSeverityTask(Task task, TimeTask timeTask)
        {
            if (!ModelState.IsValid || task.Id != timeTask.TaskId || db.TaskExists(task.Id))
            {
                return BadRequest(ModelState);
            }

            db.Tasks.Add(task);
            db.TimeTasks.Add(timeTask);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = timeTask.TaskId }, timeTask);
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