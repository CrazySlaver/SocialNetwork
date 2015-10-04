using System.Web.Mvc;
using MusicWave.Models;

namespace MusicWave.Helpers
{
    public class UserModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var user = base.BindModel(controllerContext, bindingContext) as CustomUser;
            if (user == null)
            {
                return null;
            }
        }
    }
}