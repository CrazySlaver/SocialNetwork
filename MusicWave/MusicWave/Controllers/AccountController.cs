using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.ConnectToDB;
using MusicWave.Helpers;
using MusicWave.Models;

namespace MusicWave.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManipulation _user = new UserManipulation();
        private CustomUser _customUser = new CustomUser();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase file, [ModelBinder(typeof(UserModelBinder))] CustomUser model)
        {
            if (ModelState.IsValid)
            {
                _user.AddUserToDb(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect.");
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult LogIn(LogInUser model)
        {
            if (ModelState.IsValid)
            {
                var user = IsValid(model.Email, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    TempData["user"] = user;
                    return RedirectToAction("Index", "User");
                    
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Login data is incorrect.");
                }

            }
            return RedirectToAction("Index", "Home", model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

       

        private User IsValid(string email, string password)
        {
            User user = null;
            using (var db = new WorldDBEntities2())
            {
                try
                {
                    user = db.User.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        var tempPassword = Crypto.SHA256(password);
                        if (user.Password == tempPassword)
                        {
                            return user;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}