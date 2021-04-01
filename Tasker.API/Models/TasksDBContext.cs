namespace Tasker.API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TasksDBContext : DbContext
    {
        public TasksDBContext()
            : base("name=TasksDBContext")
        {
        }

        public virtual DbSet<SeverityTask> SeverityTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TimeTask> TimeTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.Descript)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .Property(e => e.TaskType)
                .IsUnicode(false);

            modelBuilder.Entity<Task>()
                .HasOptional(e => e.SeverityTask)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Task>()
                .HasOptional(e => e.TimeTask)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete();
        }
    }
}
