using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task_manager.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }
        [DataType(DataType.MultilineText)]
        public string TaskDescription { get; set; }
        public DateTime StartDate { get; set; }
        public Single EstimatedTime { get; set; }
        public int? GroupGroupId { get; set; }
        public TaskGroup Group { get; set; }
        public int? StatusStatusId { get; set; }
        public TaskStatus Status { get; set; }
        public int? PriorityPriorityId { get; set; }
        public TaskPriority Priority { get; set; }

        public ICollection<TaskSubelement> TaskSubelements { get; set; }
        public virtual ICollection<User> Owners { get; set; }

        public Task()
        {
            TaskSubelements = new List<TaskSubelement>();
            Owners = new List<User>();
        }
    }
}