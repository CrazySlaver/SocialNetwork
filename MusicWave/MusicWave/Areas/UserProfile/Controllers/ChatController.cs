using System.Web.Mvc;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    public partial class ChatController : Controller
    {
        //TODO оформить страницу чата
        // GET: Chat
        [Authorize(Roles = "user")]
        [AccessToUserPageActionFilter]
        public virtual ActionResult Index()
        {
            var user = (User)TempData["user"];
            return View(user);
        }
    }
}