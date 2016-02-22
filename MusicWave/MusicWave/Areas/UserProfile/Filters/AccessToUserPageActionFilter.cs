using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using MusicWave.Areas.UserProfile.DAL;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Filters
{
    public class AccessToUserPageActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            User user = null;
            using (var db = new PeopleDBEntities())
            {
                try
                {
                    HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = ticket.Name;
                    user = db.User.FirstOrDefault(e => e.Email == email);
                }
                catch (NullReferenceException)
                {
                    new HttpException(403, "Forbidden");
                }

            }
        }
    }
}