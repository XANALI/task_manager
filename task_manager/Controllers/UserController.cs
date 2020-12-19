using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using task_manager.Models;
using task_manager.Auth;

namespace task_manager.Controllers
{
    [Authorize(Users = "admin@gmail.com")]
    public class UserController : Controller
    {
        TaskManagerContext db = new TaskManagerContext();

        // GET: User
        public ActionResult Index()
        {
            IEnumerable<User> users = db.Users;
            ViewBag.Users = users;
            return View("Index");
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

        [HttpPost]
        public JsonResult IsAlreadySigned(string Email)
        {

            return Json(!db.Users.Any(x => x.Email == Email), JsonRequestBehavior.AllowGet);
        }

        public bool IsUserAvailable(string Email)
        {
            IEnumerable<User> Users = db.Users;
            var RegEmailId = (from u in Users
                              where u.Email.ToUpper() == Email.ToUpper()
                              select new { Email }).FirstOrDefault();

            bool status = true;
            if (RegEmailId != null)
            {
                status = false;
            }

            return status;
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            User user = new User();

            return View(user);
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Redirect("/User/Index");
            }
            return View(user);
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
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/User/Index");
            }
            return View(user);
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