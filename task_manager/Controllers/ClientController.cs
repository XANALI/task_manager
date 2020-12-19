using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyBoards()
        {
            User user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

            IEnumerable<TaskBoard> taskBoards = db.TaskBoards.Include(t => t.Members).Where(b => b.OwnerUserId == user.UserId);

            return PartialView(taskBoards);
        }

        public ActionResult MemberBoards()
        {

            IEnumerable<TaskBoard> taskBoards = db.TaskBoards.Include(t => t.Members).Where(t => t.Members.FirstOrDefault(m => m.Email == User.Identity.Name) != null).ToList();

            return PartialView(taskBoards);
        }

        public ActionResult BoardGroups(int id)
        {
            IEnumerable<TaskGroup> taskGroups = db.TaskGroups.Where(t => t.BoardBoardId == id);

            return View(taskGroups);
        }

        [HttpGet]
        public ActionResult AddTaskBoard()
        {

            ViewBag.Members = db.Users.ToList();

            TaskBoard taskBoard = new TaskBoard();
            taskBoard.OwnerUserId = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name).UserId;

            return View(taskBoard);
        }

        [HttpPost]
        public ActionResult PostAddTaskBoard(TaskBoard taskBoard, int[] selectedMembers)
        {
            if (selectedMembers != null)
            {
                foreach (var m in db.Users.Where(member => selectedMembers.Contains(member.UserId)))
                {
                    taskBoard.Members.Add(m);
                }
            }


            db.TaskBoards.Add(taskBoard);
            db.SaveChanges();

            return Redirect("/Client/Index");
        }
    }
}