using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MusicWave.Areas.UserProfile.DAL;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    public partial class UserBaseController : Controller
    {
        private User _user = null;

        public User User
        {
            get { return _user; }
        }

        protected override void Initialize(RequestContext requestContext)
        {
            ControllerContext = new ControllerContext(requestContext, this);

            using (var db = new PeopleDBEntities())
            {
                try
                {
                    HttpCookie authCookie = requestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                    string email = ticket.Name;
                    _user = db.User.FirstOrDefault(e => e.Email == email);
                }
                catch (NullReferenceException)
                {
                    new HttpException(403, "Forbidden");
                }

            }
        }
    }
}