using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    [Authorize]
    public partial class UserController : Controller
    {
        //TODO сделать странички для админа и суперадмина
        // GET: User
        [Authorize(Roles = "user")]
        [AccessToUserPageActionFilter]
        public virtual ActionResult Index()
        {
            var user = (User)TempData["user"];
            return View(user);
        }
    }
}