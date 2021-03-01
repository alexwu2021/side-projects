using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Reminder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TaskId { get; set; }
        public int ReminderTypeId { get; set; }
        public int FirstReminderLeadTimeInMinute { get; set; }
        public int NumberOfRetries { get; set; }
        public int FrequencyInMinute { get; set; }

        public virtual ReminderType ReminderType { get; set; }
        public virtual Task Task { get; set; }
    }
}
