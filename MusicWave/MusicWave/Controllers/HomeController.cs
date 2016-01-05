using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MusicWave.Models;

namespace MusicWave.Controllers
{
    public partial class HomeController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    if (Roles.IsUserInRole("user"))
                    {
                        return RedirectToAction(MVC.UserProfile.User.Index());
                    }
                    if (Roles.IsUserInRole("admin"))
                    {
                        return RedirectToAction(MVC.AdminProfile.Admin.Index());
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public virtual ActionResult Index(LogInUser model)
        {
            if (Request.IsAuthenticated)
            {
                if (Roles.IsUserInRole("user"))
                {
                    return RedirectToAction(MVC.UserProfile.User.Index());
                }
                if (Roles.IsUserInRole("admin"))
                {
                    return RedirectToAction(MVC.AdminProfile.Admin.Index());
                }
            }
            //var error = TempData["Result"];
            return View(model);
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