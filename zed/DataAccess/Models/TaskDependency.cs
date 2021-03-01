using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class TaskDependency
    {
        public int Id { get; set; }
        public int DependingTaskId { get; set; }
        public int TaskId { get; set; }

        public virtual Task DependingTask { get; set; }
        public virtual Task Task { get; set; }
    }
}
