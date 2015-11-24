using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWave.Models;

namespace MusicWave.Controllers
{
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "This is the test project created on ASP.NET MVC framework. For those people who want to take me a job :)";

            return View();
        }
        public virtual ActionResult Contact()
        {
            return View();
        }
    }
}