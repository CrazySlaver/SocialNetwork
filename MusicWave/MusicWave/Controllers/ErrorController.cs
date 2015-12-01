using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicWave.Controllers
{
    public partial class ErrorController : Controller
    {
        // GET: Error
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public virtual ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }

        public virtual ActionResult NotAllowed()
        {
            Response.StatusCode = 401;
            return RedirectToAction(MVC.Home.Index());
        }

        public virtual ViewResult ServerError()
        {
            Response.StatusCode = 500;
            return View("ServerError");
        }
    }
}