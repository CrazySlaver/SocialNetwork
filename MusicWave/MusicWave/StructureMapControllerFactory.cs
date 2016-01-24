using System;
using System.Web;
using StructureMap;
using System.Web.Mvc;
using System.Web.Routing;

namespace MusicWave
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, "Not Found");
            }
            return ObjectFactory.GetInstance(controllerType) as IController;
            
        }
    }
}