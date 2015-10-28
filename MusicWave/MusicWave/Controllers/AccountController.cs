using System;
using System.Linq;
using System.Web;
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
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("","Login data is incorrect.");
            }
            return View(model);
            
        }

        [HttpPost]
        public ActionResult LogIn(LogInUser model)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email,false);
                   
                    //bool res = Request.IsAuthenticated;

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Login data is incorrect.");
                }

            }
            return RedirectToAction("Index","Home",model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            bool isValid = false;

            using (var db = new WorldDBEntities1())
            {
                try
                {
                    var user = db.User.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        if (user.Password == password)
                        {
                            isValid = true;
                        }
                        //if (user.Password == crypto.Compute(password, user.Password))
                        //{
                        //    isValid = true;
                        //}
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
                
            }

            return isValid;
        }
    }
}