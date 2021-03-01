using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.Serialization;

namespace Todos.Api.Models
{
    [Serializable]
    public class Todo
    {
        [DataMember]
        //[BindProperty(Name = "id", SupportsGet = true)]
        public int id { get; set; }

        [DataMember]
        //[BindProperty(Name = "name", SupportsGet = true)]
        public string name { get; set; }

        [DataMember]
        //[BindProperty(Name = "description", SupportsGet = true)]
        public string description { get; set; }

        [DataMember]
        //[BindProperty(Name = "priority", SupportsGet = true)]
        public int priority { get; set; }

        [DataMember]
        //[BindProperty(Name = "type", SupportsGet = true)]
        public int type { get; set; }

        [DataMember]
        //[BindProperty(Name = "startTime", SupportsGet = true)]
        public DateTime startTime { get; set; }

        [DataMember]
        //[BindProperty(Name = "durationInMinutes", SupportsGet = true)]
        public int durationInMinutes { get; set; }

        [DataMember]
        //[BindProperty(Name = "isReminderRequired", SupportsGet = true)]
        public bool isReminderRequired { get; set; }

        [DataMember]
        //[BindProperty(Name = "reminderMethod", SupportsGet = true)]
        public int reminderMethod { get; set; }

        [DataMember]
        //[BindProperty(Name = "reminderFirstStartTime", SupportsGet = true)]
        public DateTime reminderFirstStartTime { get; set; }

        [DataMember]
        //[BindProperty(Name = "reminderFrequency", SupportsGet = true)]
        public int reminderFrequency { get; set; }

        [DataMember]
        //[BindProperty(Name = "reminderTotalRetries", SupportsGet = true)]
        public int reminderTotalRetries { get; set; }
        
        public Todo() { }

        internal static Todo CreateTodo(DataAccess.Models.Task task, int depCount, bool needReminder = false) 
        {
            Todo ret = new Todo();
            ret.id = task.Id;
            ret.name = task.Name;
            ret.description = task.Description;
            ret.priority = task.TaskPriorityId;
            ret.type = task.TaskTypeId;
            ret.startTime = task.StartTime;
            ret.durationInMinutes = task.DurationInMinutes;
            ret.isReminderRequired = false;
            ret.reminderMethod = int.MinValue;
            ret.reminderFirstStartTime = DateTime.MinValue;
            ret.reminderFrequency = int.MinValue;
            ret.reminderTotalRetries = int.MinValue;
            return ret;
        }
        internal static Todo CreateTodo(DataAccess.Models.Task task, int depCount, int ReminderMethod, 
            DateTime ReminderFirstStartTime, int ReminderFrequency, int ReminderTotalRetries)
        {
            Todo ret = new Todo();
            ret.id = task.Id;
            ret.name = task.Name;
            ret.description = task.Description;
            ret.priority = task.TaskPriorityId;
            ret.type = task.TaskTypeId;
            ret.startTime = task.StartTime;
            ret.durationInMinutes = task.DurationInMinutes;

            ret.isReminderRequired = depCount > 0 ? true : false;
            if (ret.isReminderRequired)
            {
                ret.reminderMethod = ReminderMethod;
                ret.reminderFirstStartTime = ReminderFirstStartTime;
                ret.reminderFrequency = ReminderFrequency;
                ret.reminderTotalRetries = ReminderTotalRetries;
            }
            else
            {
                ret.reminderMethod = int.MinValue;
                ret.reminderFirstStartTime = DateTime.MinValue;
                ret.reminderFrequency = int.MinValue;
                ret.reminderTotalRetries = int.MinValue;
            }
            return ret;
        }
    }
}
