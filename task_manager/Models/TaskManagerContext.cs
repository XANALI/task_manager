using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace task_manager.Models
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskOwner> TaskOwners { get; set; }
        public DbSet<TaskSubelement> TaskSubelements { get; set; }
        public DbSet<SubelementOwner> SubelementOwners { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
    }
}