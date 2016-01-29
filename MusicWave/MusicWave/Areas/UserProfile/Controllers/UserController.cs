using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.Areas.Account.AccessToDB;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    //TODO сделать странички для админа и суперадмина
    [Authorize(Roles = "user")]
    public partial class UserController : Controller
    {
        private IUserDb _userDb;
        // GET: User
        public UserController(IUserDb userDb)
        {
            _userDb = userDb;
        }
        [AccessToUserPageActionFilter]
        public virtual ActionResult Index()
        {
            var user = (User)TempData["user"];
            return View(user);
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult Friends()
        {
            var user = (User)TempData["user"];
            return View(user);
        }

        public virtual ActionResult GetUsers(string name)
        {
            if (Request.IsAjaxRequest())
            {
                IEnumerable<User> users = _userDb.GetSeekingUser(name);
                return PartialView(users);
            }
            return View();
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult AddUserToFriend(Guid friendId)
        {
            var currentUser = (User)TempData["user"];

            return RedirectToAction(MVC.UserProfile.User.Index());
        }
    }
}