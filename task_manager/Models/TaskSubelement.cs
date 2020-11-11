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
        public int StatusId { get; set; }
        public int PriorityId { get; set; }
        public int TaskId { get; set; }

    }
}