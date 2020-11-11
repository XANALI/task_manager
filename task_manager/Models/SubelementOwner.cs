using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_manager.Models
{
    public class SubelementOwner
    {
        public int SubelementOwnerId { get; set; }
        public int SubelementId { get; set; }
        public int OwnerId { get; set; }

    }
}