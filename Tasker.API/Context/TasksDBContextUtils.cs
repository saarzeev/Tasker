using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tasker.API.Context
{
    public partial class TasksDBContext
    {
        internal bool TaskExists(int id)
        {
            return this.Tasks.Count(e => e.Id == id) > 0;
        }

        internal bool SeverityTaskExists(int id)
        {
            return this.SeverityTasks.Count(e => e.TaskId == id) > 0;
        }

        internal bool TimeTaskExists(int id)
        {
            return this.TimeTasks.Count(e => e.TaskId == id) > 0;
        }
    }
}