using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using EntityFramework.Extensions;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.DAL
{
    public class DbInfo : IDbInfo
    {
        public IEnumerable<User> GetNotifications(Guid userId)
        {
            IEnumerable<User> notify;

            using (var db = new PeopleDBEntities())
            {
                var notifications = (from n in db.FriendRelationship
                                     where userId == n.UserId && n.status == false
                                     join u in db.User on n.FriendId equals u.Id
                                     select u).ToList();
                notify = notifications;

            }
            return notify;

        }

        public void AcceptFriendship(Guid userId, Guid friendId)
        {
            using (var db = new PeopleDBEntities())
            {   
                //TODO Вывод в лог
                var accenpt = db.FriendRelationship.Where(f => f.UserId == userId && f.FriendId == friendId && f.status == false).Update(f => new FriendRelationship{ status = true});
                var accenptі = db.FriendRelationship.Where(f => f.UserId == friendId && f.FriendId == userId && f.status == false).Update(f => new FriendRelationship { status = true });
                db.SaveChanges();
            }

        }

        public void RejectFriendship(Guid userId, Guid friendId)
        {
            using (var db = new PeopleDBEntities())
            {
                //TODO Вывод в лог
                var accenpt = db.FriendRelationship.Where(f => f.UserId == userId && f.FriendId == friendId && f.status == false).Delete();
                var accenpti = db.FriendRelationship.Where(f => f.UserId == friendId && f.FriendId == userId && f.status == false).Delete();
                db.SaveChanges();
            }
        }

        public IEnumerable<User> GetFriends(Guid userId)
        {
            IEnumerable<User> friends = null;
            using (var db = new PeopleDBEntities())
            {
                var friendList = (from f in db.FriendRelationship
                            where f.UserId == userId && f.status == true
                            join u in db.User on f.FriendId equals u.Id
                            select u).ToList();
                friends = friendList;
            }
            return friends;
        }

        public static User CheckUserCookie(ActionExecutingContext context)
        {
            User user = null;
            using (var db = new PeopleDBEntities())
            {
                try
                {
                    HttpCookie authCookie = context.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = ticket.Name;
                    user = db.User.FirstOrDefault(e => e.Email == email);
                }
                catch (NullReferenceException)
                {
                    new HttpException(403, "Forbidden");
                }

            }
            return user;
        }

        public static User CheckUserCookie(RequestContext context)
        {
            User user = null;
            using (var db = new PeopleDBEntities())
            {
                try
                {
                    HttpCookie authCookie = context.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = ticket.Name;
                    user = db.User.FirstOrDefault(e => e.Email == email);
                }
                catch (NullReferenceException)
                {
                    new HttpException(403, "Forbidden");
                }

            }
            return user;
        }
        
    }
}

