using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task_manager.Models
{
    public class TaskGroup
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        [DataType(DataType.MultilineText)]
        public string GroupDescription { get; set; }
        public int? BoardBoardId { get; set; }
        public TaskBoard Board { get; set; }
        public ICollection<Task> Tasks { get; set; }

        public TaskGroup()
        {
            Tasks = new List<Task>();
        }
    }
}