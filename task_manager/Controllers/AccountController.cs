using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using task_manager.Models;

namespace task_manager.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (TaskManagerContext db = new TaskManagerContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);

                    if(user.Email == "admin@gmail.com")
                    {
                        RedirectToAction("Index", "User");
                    }

                    if(ReturnUrl != null)
                    {
                        string controllerName = ReturnUrl.Split('/')[1];

                        return RedirectToAction(ReturnUrl.Substring(controllerName.Length + 1), controllerName);
                    }

                    return RedirectToAction("Index", "Client");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        private void sendEmail(User user)
        {
            var senderEmail = new MailAddress("csse1806k@gmail.com", "Task Manager Group");
            var receiverEmail = new MailAddress(user.Email, "Receiver");
            string password = "csse123456789";
            string subject = "Welcome to our Group!";
            string body = "We are glad to see you in our application. You can contact us using this email sender.";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail) {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(mess);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (TaskManagerContext db = new TaskManagerContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (TaskManagerContext db = new TaskManagerContext())
                    {
                        db.Users.Add(new User { Email = model.Email, Password = model.Password, Username = "-", FirstName = "-", LastName = "-" });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        sendEmail(user);
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}