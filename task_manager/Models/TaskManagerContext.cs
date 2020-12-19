using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace task_manager.Models
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskBoard> TaskBoards { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskSubelement> TaskSubelements { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>().HasMany(u => u.MemberBoards)
                .WithMany(m => m.Members)
                .Map(t => t.MapLeftKey("MemberId")
                .MapRightKey("BoardId")
                .ToTable("BoardMembers"));

            modelBuilder.Entity<TaskSubelement>().HasMany(t => t.Owners)
                .WithMany(o => o.TaskSubelements)
                .Map(table => table.MapLeftKey("SubelementId")
                .MapRightKey("OwnerId")
                .ToTable("SubelementOwners"));

            modelBuilder.Entity<Task>().HasMany(t => t.Owners)
                .WithMany(o => o.Tasks)
                .Map(table => table.MapLeftKey("TaskId")
                .MapRightKey("OwnerId")
                .ToTable("TaskOwners"));
        }
    }

}