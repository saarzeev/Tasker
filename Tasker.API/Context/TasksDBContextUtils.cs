using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasker.API.Context
{
    public partial class TasksDBContext
    {
        public bool SeverityTaskExists(int id)
        {
            return this.SeverityTasks.Count(e => e.TaskId == id) > 0;
        }

        public bool TaskExists(int id)
        {
            return this.Tasks.Count(e => e.Id == id) > 0;
        }
    }
}