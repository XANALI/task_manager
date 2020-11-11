using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_manager.Models
{
    public class TaskOwner
    {
        public int TaskOwnerId { get; set; }
        public int TaskId { get; set; }
        public int OwnerId { get; set; }
    }
}