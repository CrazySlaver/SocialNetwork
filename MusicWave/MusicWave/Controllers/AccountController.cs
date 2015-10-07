﻿using System.Web;
using System.Web.Mvc;
using MusicWave.ConnectToDB;
using MusicWave.Helpers;
using MusicWave.Models;

namespace MusicWave.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManipulation _user = new UserManipulation();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HttpPostedFileBase file, [ModelBinder(typeof(UserModelBinder))] CustomUser model)
        {
            _user.AddUserToDb(model);
            return View("SignIn", model);
            //if (ModelState.IsValid)
            //{
               
            //}
            //return View("Register", model);
        }
        [HttpPost]
        public ActionResult SignIn(HttpPostedFileBase file, [ModelBinder(typeof (UserModelBinder))] CustomUser model)
        {
            _user.AddUserToDb(model);
            return View();
        }
        
    }
}