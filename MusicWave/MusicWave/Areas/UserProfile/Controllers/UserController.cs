using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using MusicWave.Areas.Account.AccessToDB;
using MusicWave.Areas.UserProfile.DAL;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    //TODO сделать странички для админа и суперадмина
    [Authorize(Roles = "user")]

    public partial class UserController : Controller
    {
        private IUserDb _userDb;
        private IDbInfo _dbInfo;
        // GET: User
        public UserController(IUserDb userDb, IDbInfo dbInfo)
        {
            _userDb = userDb;
            _dbInfo = dbInfo;
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
            if (friendId != null)
            {
                bool flag = _userDb.AddUserToFriend(currentUser.Id, friendId);
                if (flag)
                {
                    //return PartialView("_OkButton");
                    return Content("<img class=\"media-object btn-add\" src=\"/Content/Buttons/ok.svg\" width=\"32\" height=\"32\" id=\"imgadd\"/>");
                }
                else
                {
                    return PartialView("_MessageBox");
                }
            }
            return RedirectToAction(MVC.UserProfile.User.Index());
        }
        //TODO логирование и юнит тесты
        //TODO убрать AccessToUserPageActionFilter (кастомный контроллер)
        [AccessToUserPageActionFilter]
        public virtual ActionResult GetNotification()
        {
            var user = (User)TempData["user"];
            IEnumerable<User> notifications = _dbInfo.GetNotifications(user.Id);
            //var tuple = new Tuple<User, IEnumerable<Notification>>(user, notifications);
            return PartialView("_Notification", notifications);
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult AcceptFriendship(Guid friendId)
        {
            var user = (User)TempData["user"];
            _dbInfo.AcceptFriendship(user.Id, friendId);
            return new EmptyResult();
            //return Content(friendId.ToString(), "application/json");
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult RejectFriendship(Guid friendId)
        {
            var user = (User)TempData["user"];
            _dbInfo.RejectFriendship(user.Id,friendId);
            return new EmptyResult();
        }
    }
}