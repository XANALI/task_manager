using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    [Authorize(Users = "admin@gmail.com")]
    public class TaskController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<Task> tasks = db.Tasks;

            string result = "Вы не авторизованы";
            if (User.Identity.IsAuthenticated)
            {
                result = "Ваш логин: " + User.Identity.Name;
            }
            ViewBag.AuthText = result;

            return View(tasks);
        }

        [HttpGet]
        public ActionResult ShowTask(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Task task = db.Tasks.Include(t => t.Group).FirstOrDefault(t => t.TaskId == id);
            task.Status = db.TaskStatuses.Find(task.StatusStatusId);
            task.Priority = db.TaskPriorities.Find(task.PriorityPriorityId);

            return View(task);
        }

        [HttpGet]
        public ActionResult AddTask()
        {

            SelectList groups = new SelectList(db.TaskGroups.Include(t => t.Board), "GroupId", "GroupName");
            ViewBag.TaskGroups = groups;

            SelectList statuses = new SelectList(db.TaskStatuses, "StatusId", "StatusName");
            ViewBag.TaskStatuses = statuses;

            SelectList priorities = new SelectList(db.TaskPriorities, "PriorityId", "PriorityName");
            ViewBag.TaskPriorities = priorities;

            return View();
        }

        [HttpPost]
        public ActionResult PostAddTask(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();

            return Redirect("/Task/Index");
        }

        [HttpGet]
        public ActionResult EditTask(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Task task = db.Tasks.Include(t => t.Group).FirstOrDefault(t => t.TaskId == id);
            if (task != null)
            {
                SelectList groups = new SelectList(db.TaskGroups.Include(t => t.Board), "GroupId", "GroupName", task.GroupGroupId);
                ViewBag.TaskGroups = groups;

                SelectList statuses = new SelectList(db.TaskStatuses, "StatusId", "StatusName", task.StatusStatusId);
                ViewBag.TaskStatuses = statuses;

                SelectList priorities = new SelectList(db.TaskPriorities, "PriorityId", "PriorityName", task.PriorityPriorityId);
                ViewBag.TaskPriorities = priorities;

                return View(task);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditTask(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Task/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteTask(int id)
        {
            Task task = new Task { TaskId = id };
            db.Entry(task).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/Task/Index");
        }
    }
}