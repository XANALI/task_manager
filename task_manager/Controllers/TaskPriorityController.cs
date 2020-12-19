using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    public class TaskPriorityController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<TaskPriority> taskPriorities = db.TaskPriorities;
            return View(taskPriorities);
        }

        [HttpGet]
        public ActionResult ShowTaskPriority(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TaskPriority taskPriority = db.TaskPriorities.Find(id);

            return View(taskPriority);
        }

        [HttpGet]
        public ActionResult AddTaskPriority()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostAddTaskPriority(TaskPriority taskPriority)
        {
            db.TaskPriorities.Add(taskPriority);
            db.SaveChanges();

            return Redirect("/TaskPriority/Index");
        }

        [HttpGet]
        public ActionResult EditTaskPriority(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TaskPriority taskPriority = db.TaskPriorities.Find(id);
            if (taskPriority != null)
            {
                return View(taskPriority);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditTaskPriority(TaskPriority taskPriority)
        {
            db.Entry(taskPriority).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/TaskPriority/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteTaskPriority(int id)
        {
            TaskPriority taskPriority = new TaskPriority { PriorityId = id };
            db.Entry(taskPriority).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/TaskPriority/Index");
        }
    }
}