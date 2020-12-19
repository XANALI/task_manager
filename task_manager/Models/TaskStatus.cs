using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task_manager.Models
{
    public class TaskStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public TaskStatus()
        {
            Tasks = new List<Task>();
        }
    }
}