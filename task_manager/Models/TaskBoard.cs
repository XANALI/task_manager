using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task_manager.Models
{
    public class TaskBoard
    {
        [Key]
        public int BoardId { get; set; }
        public string BoardName { get; set; }
        [DataType(DataType.MultilineText)]
        public string BoardDescription { get; set; }
        public int? OwnerUserId { get; set; }
        public User Owner { get; set; }
        public ICollection<TaskGroup> TaskGroups { get; set; }

        public TaskBoard()
        {
            TaskGroups = new List<TaskGroup>();
        }
    }
}