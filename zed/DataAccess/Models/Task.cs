using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Task
    {
        public Task()
        {
            Reminders = new HashSet<Reminder>();
            TaskDependencyDependingTasks = new HashSet<TaskDependency>();
            TaskDependencyTasks = new HashSet<TaskDependency>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TaskPriorityId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }
        public bool NeedReminder { get; set; }

        public virtual TaskPriority TaskPriority { get; set; }
        public virtual TaskType TaskType { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
        public virtual ICollection<TaskDependency> TaskDependencyDependingTasks { get; set; }
        public virtual ICollection<TaskDependency> TaskDependencyTasks { get; set; }
    }
}
