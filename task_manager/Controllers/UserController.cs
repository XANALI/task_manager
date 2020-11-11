using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;

namespace task_manager.Controllers
{
    public class UserController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<User> users = db.Users;
            ViewBag.Users = users;
            return View();
        }

        [HttpGet]
        public ActionResult ShowUser(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            User user = db.Users.Find(id);

            return View(user);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostAddUser()
        {
            string username = Request["username"];
            string password = Request["password"];
            string firstName = Request["firstName"];
            string lastName = Request["lastName"];
            string email = Request["email"];

            User user = new User();
            user.Username = username;
            user.Password = password;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = email;

            db.Users.Add(user);
            db.SaveChanges();

            return Redirect("/User/Index");
        }

        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            User user = db.Users.Find(id);
            if(user != null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult PostEditUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/User/Index");
        }

        [HttpPost]
        public ActionResult PostDeleteUser(int id)
        {
            User user = new User { UserId = id };
            db.Entry(user).State = EntityState.Deleted;
            db.SaveChanges();

            return Redirect("/User/Index");
        }
    }
}