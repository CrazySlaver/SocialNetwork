using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicWave.ConnectToDB;
using MusicWave.Models;

namespace MusicWave.Helpers
{
    public static class AuthHelper
    {
        public static void LogInUser(HttpContextBase httpContext, string cookies)
        {
            var cookie = new HttpCookie("__AUTH")
            {
                Value = cookies,
                Expires = DateTime.Now.AddYears(1)
            };

            httpContext.Response.Cookies.Add(cookie);
        }
        public static void LogOffUser(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies["__AUTH"] != null)
            {
                var cookie = new HttpCookie("__AUTH")
                {
                    Expires = DateTime.Now.AddDays(-1),
                };

                httpContext.Response.Cookies.Add(cookie);
            }
        }
        public static User GetUser(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];

            if (authCookie != null)
            {
                User user = DataBase.GetUserByCookeis(authCookie.Value);

                return user;
            }

            return null;
        }
        public static bool IsAuthenticated(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["__AUTH"];

            if (authCookie != null)
            {
                User user = DataBase.GetUserByCookeis(authCookie.Value);

                return user != null;
            }

            return false;
        }
        public static User GetUserByCookeis(string cookies)
        {
            var entity = new WorldDBEntities();

            return entity.User.FirstOrDefault(u => u.Cookies == cookies);
        }
    }
}