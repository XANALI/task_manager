using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    public class TaskStatusController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<TaskStatus> taskStatuses = db.TaskStatuses;
            return View(taskStatuses);
        }

        [HttpGet]
        public ActionResult ShowTaskStatus(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TaskStatus taskStatus = db.TaskStatuses.Find(id);

            return View(taskStatus);
        }

        [HttpGet]
        public ActionResult AddTaskStatus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostAddTaskStatus(TaskStatus taskStatus)
        {
            db.TaskStatuses.Add(taskStatus);
            db.SaveChanges();

            return Redirect("/TaskStatus/Index");
        }

        [HttpGet]
        public ActionResult EditTaskStatus(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TaskStatus taskStatus = db.TaskStatuses.Find(id);
            if (taskStatus != null)
            {
                return View(taskStatus);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditTaskStatus(TaskStatus taskStatus)
        {
            db.Entry(taskStatus).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/TaskStatus/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteTaskStatus(int id)
        {
            TaskStatus taskStatus = new TaskStatus { StatusId = id };
            db.Entry(taskStatus).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/TaskStatus/Index");
        }
    }
}