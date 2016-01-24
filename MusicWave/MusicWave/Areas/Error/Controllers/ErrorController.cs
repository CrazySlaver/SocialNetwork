using System.Web.Mvc;

namespace MusicWave.Controllers
{


    public partial class ErrorController : Controller
    {
     
        // GET: Error
        public virtual ViewResult Index()
        {
            int status = Response.StatusCode;
            switch (status)
            {
                case 404:
                    return View("NotFound");
                case 500:
                    return View("ServerError");

            }
            return View("Error");
        }

        public virtual ViewResult NotAllowed()
        {
            Response.StatusCode = 401;
            return View("NotAllow");
        }

        public virtual ViewResult ServerError()
        {
            Response.StatusCode = 500;
            return View("ServerError");
        }
        
        public virtual ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }
    }
}