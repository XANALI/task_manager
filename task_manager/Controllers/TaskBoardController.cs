using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    public class TaskBoardController : Controller
    {

        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            var taskBoards = db.TaskBoards.Include(t => t.Owner);
            return View(taskBoards.ToList());
        }

        [HttpGet]
        public ActionResult ShowTaskBoard(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TaskBoard taskBoard = db.TaskBoards.Include(t => t.Owner).FirstOrDefault(t => t.BoardId == id);

            return View(taskBoard);
        }

        [HttpGet]
        public ActionResult AddTaskBoard()
        {

            SelectList users = new SelectList(db.Users, "UserId", "Username");
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        public ActionResult PostAddTaskBoard(TaskBoard taskBoard)
        {
            db.TaskBoards.Add(taskBoard);
            db.SaveChanges();

            return Redirect("/TaskBoard/Index");
        }

        [HttpGet]
        public ActionResult EditTaskBoard(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            TaskBoard taskBoard = db.TaskBoards.Include(t => t.Owner).FirstOrDefault(t => t.BoardId == id);
            if (taskBoard != null)
            {
                SelectList users = new SelectList(db.Users, "UserId", "Username", taskBoard.OwnerUserId);
                ViewBag.Users = users;

                return View(taskBoard);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditTaskBoard(TaskBoard taskBoard)
        {
            db.Entry(taskBoard).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/TaskBoard/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteTaskBoard(int id)
        {
            TaskBoard taskBoard = new TaskBoard { BoardId = id };
            db.Entry(taskBoard).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/TaskBoard/Index");
        }
    }
}