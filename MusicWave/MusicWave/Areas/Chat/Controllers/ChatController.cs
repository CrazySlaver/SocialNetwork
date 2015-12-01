using System.Web.Mvc;

namespace MusicWave.Areas.Chat.Controllers
{
    public partial class ChatController : Controller
    {
        //TODO оформить страницу чата
        // GET: Chat
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}