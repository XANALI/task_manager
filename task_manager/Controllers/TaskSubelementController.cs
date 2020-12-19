using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    public class TaskSubelementController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<TaskSubelement> taskSubelements = db.TaskSubelements;
            return View(taskSubelements);
        }

        [HttpGet]
        public ActionResult ShowTaskSubelement(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TaskSubelement taskSubelement = db.TaskSubelements.Include(t => t.Task).FirstOrDefault(t => t.SubelementId == id);
            taskSubelement.Status = db.TaskStatuses.Find(taskSubelement.StatusStatusId);
            taskSubelement.Priority = db.TaskPriorities.Find(taskSubelement.PriorityPriorityId);

            return View(taskSubelement);
        }

        [HttpGet]
        public ActionResult AddTaskSubelement()
        {

            SelectList tasks = new SelectList(db.Tasks.Include(t => t.Group), "TaskId", "TaskName");
            ViewBag.Tasks = tasks;

            SelectList statuses = new SelectList(db.TaskStatuses, "StatusId", "StatusName");
            ViewBag.TaskStatuses = statuses;

            SelectList priorities = new SelectList(db.TaskPriorities, "PriorityId", "PriorityName");
            ViewBag.TaskPriorities = priorities;

            return View();
        }

        [HttpPost]
        public ActionResult PostAddTaskSubelement(TaskSubelement taskSubelement)
        {
            db.TaskSubelements.Add(taskSubelement);
            db.SaveChanges();

            return Redirect("/TaskSubelement/Index");
        }

        [HttpGet]
        public ActionResult EditTaskSubelement(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TaskSubelement taskSubelement = db.TaskSubelements.Include(t => t.Task).FirstOrDefault(t => t.SubelementId == id);
            if (taskSubelement != null)
            {
                SelectList tasks = new SelectList(db.Tasks.Include(t => t.Group), "TaskId", "TaskName", taskSubelement.TaskTaskId);
                ViewBag.Tasks = tasks;

                SelectList statuses = new SelectList(db.TaskStatuses, "StatusId", "StatusName", taskSubelement.StatusStatusId);
                ViewBag.TaskStatuses = statuses;

                SelectList priorities = new SelectList(db.TaskPriorities, "PriorityId", "PriorityName", taskSubelement.PriorityPriorityId);
                ViewBag.TaskPriorities = priorities;

                return View(taskSubelement);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditTaskSubelement(TaskSubelement taskSubelement)
        {
            db.Entry(taskSubelement).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/TaskSubelement/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteTaskSubelement(int id)
        {
            TaskSubelement taskSubelement = new TaskSubelement { SubelementId = id };
            db.Entry(taskSubelement).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/TaskSubelement/Index");
        }
    }
}