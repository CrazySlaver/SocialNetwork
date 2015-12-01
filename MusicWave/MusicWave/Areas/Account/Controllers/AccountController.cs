using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.Areas.Account.AccessToDB;
using MusicWave.Areas.Account.Filters;
using MusicWave.Areas.Account.Helpers;
using MusicWave.Models;
using reCAPTCHA.MVC;

namespace MusicWave.Areas.Account.Controllers
{
    //TODO логотип и иконка
    public partial class AccountController : Controller
    {
        private readonly UserManipulation _user = new UserManipulation();

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Register()
        {
            return View();
        }

        [HttpPost, CaptchaValidator]
        //[CustomActionFilter]
        public virtual ActionResult Register(HttpPostedFileBase file, [ModelBinder(typeof(UserModelBinder))] CustomUser model, bool? captchaValid, string captchaErrorMessage)
        {
            if (captchaValid != null && !(bool)captchaValid)
                ModelState.AddModelError("captcha", captchaErrorMessage);

            if (ModelState.IsValid)
            {
                _user.AddUserToDb(model);
                return RedirectToAction(MVC.Home.Index());
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect.");
            }
            return View(model);

        }

        [HttpPost]
        public virtual ActionResult LogIn(LogInUser model)
        {
            if (ModelState.IsValid)
            {
                var user = Security.CheckPassword(model.Email, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    TempData["user"] = user;
                    return RedirectToAction(MVC.UserProfile.User.Index());
                    
                }
                else
                {
                    ModelState.AddModelError("CustomError", "Login data is incorrect.");
                }

            }
            return RedirectToAction("Index", "Home", model);
        }

        public virtual ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.Index());
        }
    }
}