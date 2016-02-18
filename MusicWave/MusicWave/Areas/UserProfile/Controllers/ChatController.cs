using System.Web.Mvc;
using MusicWave.Areas.UserProfile.Filters;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Controllers
{
    public partial class ChatController : UserBaseController
    {
        //TODO оформить страницу чата
        
        // GET: Chat
        [Authorize(Roles = "user")]
        [AccessToUserPageActionFilter]
        public virtual ActionResult Index()
        {
            return View(User);
        }
    }
}