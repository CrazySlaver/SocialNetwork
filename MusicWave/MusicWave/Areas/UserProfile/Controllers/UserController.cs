using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MusicWave.Areas.Account.AccessToDB;
using MusicWave.Areas.UserProfile.DAL;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    //TODO сделать странички для админа и суперадмина
    [Authorize(Roles = "user")]
    public partial class UserController : UserBaseController
    {
        private IUserDb _userDb;
        private IDbInfo _dbInfo;

        public UserController(IUserDb userDb, IDbInfo dbInfo)
        {
            _userDb = userDb;
            _dbInfo = dbInfo;
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult Index()
        {
            return View(User);
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult Friends()
        {
            IEnumerable<User> friends = _dbInfo.GetFriends(User.Id);
            return View(friends);
        }

        [AccessToUserPageActionFilter]
        [ValidateAntiForgeryToken]
        public virtual ActionResult GetUsers(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                IEnumerable<User> users = _userDb.GetSeekingUser(name);
                return PartialView(users);
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult AddUserToFriend(Guid friendId)
        {
            if (friendId != null)
            {
                bool flag = _userDb.AddUserToFriend(User.Id, friendId);
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

        [AccessToUserPageActionFilter]
        public virtual ActionResult RemoveUserFromFriend(Guid friendId)
        {
            if (friendId != null)
            {
                _userDb.RemoveUserFromFriend(User.Id, friendId);
                return new EmptyResult();

            }
            return RedirectToAction(MVC.UserProfile.User.Index());
        }
        //TODO логирование и юнит тесты

        [AccessToUserPageActionFilter]
        public virtual ActionResult GetNotification()
        {
            IEnumerable<User> notifications = _dbInfo.GetNotifications(User.Id);
            //var tuple = new Tuple<User, IEnumerable<Notification>>(user, notifications);
            return PartialView("Notification", notifications);
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult AcceptFriendship(Guid friendId)
        {
            _dbInfo.AcceptFriendship(User.Id, friendId);
            return new EmptyResult();
        }

        [AccessToUserPageActionFilter]
        public virtual ActionResult RejectFriendship(Guid friendId)
        {
            _dbInfo.RejectFriendship(User.Id,friendId);
            return new EmptyResult();
        }
    }
}