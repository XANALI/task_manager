using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task_manager.Controllers
{
    public class TaskController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            var taskGroups = db.TaskGroups.Include(t => t.Board);
            return View(taskGroups.ToList());
        }

        [HttpGet]
        public ActionResult ShowTaskGroup(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TaskGroup taskGroup = db.TaskGroups.Include(t => t.Board).FirstOrDefault(t => t.GroupId == id);
            taskGroup.Board = db.TaskBoards.Include(t => t.Owner).FirstOrDefault(t => t.BoardId == taskGroup.Board.BoardId);

            return View(taskGroup);
        }

        [HttpGet]
        public ActionResult AddTaskGroup()
        {

            SelectList boards = new SelectList(db.TaskBoards.Include(t => t.Owner), "BoardId", "BoardName");
            ViewBag.TaskBoards = boards;

            return View();
        }

        [HttpPost]
        public ActionResult PostAddTaskGroup(TaskGroup taskGroup)
        {
            db.TaskGroups.Add(taskGroup);
            db.SaveChanges();

            return Redirect("/TaskGroup/Index");
        }

        [HttpGet]
        public ActionResult EditTaskGroup(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TaskGroup taskGroup = db.TaskGroups.Include(t => t.Board).FirstOrDefault(t => t.GroupId == id);
            if (taskGroup != null)
            {
                SelectList boards = new SelectList(db.TaskBoards.Include(t => t.Owner), "BoardId", "BoardName", taskGroup.BoardBoardId);
                ViewBag.TaskBoards = boards;

                return View(taskGroup);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditTaskGroup(TaskGroup taskGroup)
        {
            db.Entry(taskGroup).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/TaskGroup/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteTaskGroup(int id)
        {
            TaskGroup taskGroup = new TaskGroup { GroupId = id };
            db.Entry(taskGroup).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/TaskGroup/Index");
        }
    }
}