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
    public class UserBaseController : Controller
    {
        private User _user = null;

        public User User
        {
            get { return _user; }
        }
        
        protected override void Initialize(RequestContext requestContext)
        {
            ControllerContext = new ControllerContext(requestContext, this);
            _user = DbInfo.CheckUserCookie(requestContext);
            if (_user == null)
            {
                //TODO передача ошибки вверх по стеку
                throw new NullReferenceException();
            }
        }
    }
}