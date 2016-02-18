using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using MusicWave.Areas.UserProfile.DAL;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.Filters
{
    public class AccessToUserPageActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (DbInfo.CheckUserCookie(filterContext) == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}