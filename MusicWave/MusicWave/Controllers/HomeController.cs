using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicWave.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "This is the test project created on ASP.NET MVC framework. For those people who want to take me a job :)";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}