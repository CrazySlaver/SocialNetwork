using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWave.ConnectToDB;
using MusicWave.Helpers;
using MusicWave.Models;

namespace MusicWave.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManipulation _user = new UserManipulation();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase file, [ModelBinder(typeof(UserModelBinder))] CustomUser model)
        {
            _user.AddUserToDb(model);
            return View("Index", model);
        }
        
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(CustomUser model)
        {
            _user.AddUserToDb(model);
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            
            bool isValid = false;

            using (var db = new WorldDBEntities1())
            {
                var user = db.User.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.Password))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}