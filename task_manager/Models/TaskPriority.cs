using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task_manager.Models
{
    public class TaskPriority
    {
        [Key]
        public int PriorityId { get; set; }
        public string PriorityName { get; set; }
        public string PriorityColor { get; set; }
        public ICollection<Task> Tasks { get; set; }

        public TaskPriority()
        {
            Tasks = new List<Task>();
        }
    }
}