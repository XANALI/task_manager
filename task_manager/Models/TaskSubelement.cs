using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace task_manager.Models
{
    public class TaskSubelement
    {
        [Key]
        public int SubelementId { get; set; }
        public string SubelementName { get; set; }
        public DateTime SubelementDate { get; set; }
        public int? StatusStatusId { get; set; }
        public TaskStatus Status { get; set; }
        public int? PriorityPriorityId { get; set; }
        public TaskPriority Priority { get; set; }
        public int? TaskTaskId { get; set; }
        public Task Task { get; set; }

        public virtual ICollection<User> Owners { get; set; }

        public TaskSubelement()
        {
            Owners = new List<User>();
        }
    }
}