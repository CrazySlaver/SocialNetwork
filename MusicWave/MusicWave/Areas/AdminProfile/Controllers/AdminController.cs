using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicWave.Areas.AdminProfile.DAL;
using MusicWave.Models;

namespace MusicWave.Areas.AdminProfile.Controllers
{
    [Authorize(Roles = "admin")]
    public partial class AdminController : Controller
    {
        //TODO сделать поиск с выпадающим списком
        //TODO сделать подгрузку списка пользователей через Ajax
        // GET: AdminProfile/Admin
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult SearchResult()
        {
            var countries = new List<UserModel>
            {
                new UserModel {ShortCode = "US", Name = "United States"},
                new UserModel {ShortCode = "CA", Name = "Canada"},
                new UserModel {ShortCode = "AF", Name = "Afghanistan"},
                new UserModel {ShortCode = "AL", Name = "Albania"},
                new UserModel {ShortCode = "DZ", Name = "Algeria"},
                new UserModel {ShortCode = "DS", Name = "American Samoa"},
                new UserModel {ShortCode = "AD", Name = "Andorra"},
                new UserModel {ShortCode = "AO", Name = "Angola"},
                new UserModel {ShortCode = "AI", Name = "Anguilla"},
                new UserModel {ShortCode = "AQ", Name = "Antarctica"},
                new UserModel {ShortCode = "AG", Name = "Antigua and/or Barbuda"}
            };

            return Json(countries, JsonRequestBehavior.AllowGet);
        }


    }
}