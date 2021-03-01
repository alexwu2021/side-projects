using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class TaskPriority
    {
        public TaskPriority()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
