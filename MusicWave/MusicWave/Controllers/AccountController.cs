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

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase file, [ModelBinder(typeof(UserModelBinder))] CustomUser model)
        {
            _user.AddUserToDb(model);

            return View();
        }
    }
}