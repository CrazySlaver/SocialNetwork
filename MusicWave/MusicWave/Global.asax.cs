using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MusicWave.Areas.Account.AccessToDB;
using MusicWave.Areas.Account.Helpers;
using MusicWave.Areas.UserProfile.DAL;
using StructureMap;
using MusicWave.Models;

namespace MusicWave
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(CustomUser), new UserModelBinder());

            ObjectFactory.Initialize(cfg =>
            {
                cfg.For<IUserDb>().Use<UserDb>();
                cfg.For<IDbInfo>().Use<DbInfo>();
            });
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            //DependencyResolver.SetResolver(new StructureMapDependencyResolver());

            
        }
        #region if error
        //protected void Application_Error(object sender, EventArgs e)
        //{

        //    if (Context.IsCustomErrorEnabled)
        //        ShowCustomErrorPage(Server.GetLastError());
        //}



        //private void ShowCustomErrorPage(Exception exception)
        //{

        //    HttpException httpException = exception as HttpException;
        //    if (httpException == null)
        //        httpException = new HttpException(500, "Internal Server Error", exception);



        //    Response.Clear();
        //    RouteData routeData = new RouteData();
        //    routeData.Values.Add("controller", "Error");
        //    routeData.Values.Add("fromAppErrorEvent", true);

        //    switch (httpException.GetHttpCode())
        //    {
        //        case 403:
        //            routeData.Values.Add("action", "AccessDenied");
        //            break;

        //        case 404:
        //            routeData.Values.Add("action", "NotFound");
        //            break;

        //        case 500:
        //            routeData.Values.Add("action", "ServerError");
        //            break;

        //        default:
        //            routeData.Values.Add("action", "OtherHttpStatusCode");
        //            routeData.Values.Add("httpStatusCode", httpException.GetHttpCode());
        //            break;
        //    }
        //    Server.ClearError();
            
        //    IController controller = new ErrorController();
        //    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));

        //}
        #endregion

    }
}
