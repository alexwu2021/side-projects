using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class TaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
