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


        public void Transcribe(Task other)
        {
            this.Name = other.Name;
            this.Description = other.Description;

            this.TaskPriorityId = other.TaskPriorityId;
            this.TaskPriority = other.TaskPriority;
            this.TaskTypeId = other.TaskTypeId;
            this.TaskType = other.TaskType;
            this.StartTime = other.StartTime;
            this.DurationInMinutes = other.DurationInMinutes;
            this.NeedReminder = other.NeedReminder;
            
            this.Reminders.Clear();
            foreach (var rdr in other.Reminders)
                this.Reminders.Add(rdr);
            
            this.TaskDependencyDependingTasks.Clear();
            foreach (var depending in other.TaskDependencyDependingTasks)
                this.TaskDependencyDependingTasks.Add(depending);

            this.TaskDependencyTasks.Clear();
            foreach (var dependent in other.TaskDependencyTasks)
                this.TaskDependencyTasks.Add(dependent);

        }

    }
}
