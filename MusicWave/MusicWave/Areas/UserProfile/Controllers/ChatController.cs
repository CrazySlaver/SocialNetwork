using System.Web.Mvc;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    public partial class ChatController : Controller
    {
        //TODO оформить страницу чата
        //TODO сделать AJAX подрузку контента страницы
        //TODO оформить страницу друзей (передача списка друзей на страницу) со всеми данным

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