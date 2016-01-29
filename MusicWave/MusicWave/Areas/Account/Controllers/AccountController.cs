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
        private IUserDb _userDb;
        public AccountController(IUserDb userDb)
        {
            _userDb = userDb;
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Register()
        {
            return View();
        }

        //TODO после введения неправильных данных прикрипленная картинка теряется
        [HttpPost, CaptchaValidator]
        //[CustomActionFilter]
        public virtual ActionResult Register(HttpPostedFileBase file, [ModelBinder(typeof(UserModelBinder))] CustomUser model, bool? captchaValid, string captchaErrorMessage)
        {
            string error = "Registration data is incorrect.";
            if (captchaValid != null && !(bool)captchaValid)
            {
                error = error + "Check the captcha.";

            }
            if (ModelState.IsValid)
            {
                bool checkEmail = _userDb.CheckEmail(model.Email);
                if (checkEmail)
                {
                    _userDb.AddUserToDb(model);
                    return RedirectToAction(MVC.Home.Home.Index());
                } 
                error = "Email is already exist.";
                ModelState.AddModelError("", error);
                
            }
            else
            {
                ModelState.AddModelError("", error);
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