using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.Areas.Account.AccessToDB;
using MusicWave.Areas.Account.Helpers;
using MusicWave.Models;
using MusicWave.Providers;
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
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect.");
            }
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogIn(LogInUser model)
        {
            //string error;
            if (ModelState.IsValid)
            {
                var user = Security.CheckPasswordAndRole(model.Email, model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    TempData["user"] = user;
                    var role = new MusicWaveRoleProviders();
                    bool isUser = role.IsUserInRole(user.Email, "user");
                    if (isUser)
                    {
                        return RedirectToAction("Index", "User", new {area="UserProfile"});
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin",new{area="AdminProfile"});
                    }
                }
                else
                {
                    //TODO исправить ошибку при вводе некоректного логина и пароля при входе на сайт
                    //TempData["Result"] = model;
                    ModelState.AddModelError("", "Login data is incorrect.");
                }

            }
            //return View("Index","Home", new{area="",model});
            return RedirectToAction("Index","Home", new{area="Home",model});
        }

        public virtual ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home",new {area="Home"});
        }
    }
}