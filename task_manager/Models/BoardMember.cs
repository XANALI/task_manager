using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace task_manager.Models
{
    public class BoardMember
    {
        public int BoardMemberId { get; set; }
        public int BoardId { get; set; }
        public int MemberId { get; set; }

    }
}