using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.ConnectToDB;
using MusicWave.Helpers;
using MusicWave.Models;

namespace MusicWave.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManipulation _user = new UserManipulation();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(HttpPostedFileBase file, [ModelBinder(typeof (UserModelBinder))] CustomUser model)
        {
            _user.AddUserToDb(model);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(CustomUser user)
        {
            return View("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();  
            return RedirectToAction("Index", "Home");
        }
    }
}