namespace Tasker.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Descript { get; set; }

        [Required]
        [StringLength(255)]
        public string TaskType { get; set; }

        public virtual SeverityTask SeverityTask { get; set; }

        public virtual TimeTask TimeTask { get; set; }
    }
}
