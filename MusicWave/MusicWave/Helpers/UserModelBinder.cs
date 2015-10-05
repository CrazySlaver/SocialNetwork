using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MusicWave.Models;

namespace MusicWave.Helpers
{
    public class UserModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var user = base.BindModel(controllerContext, bindingContext) as CustomUser;

            var request = controllerContext.HttpContext.Request;
            var files = request.Files;

            if (files.Count == 0)
                return user;

            var file = files.Get(0);
            var checkFileResult = CheckFile(file);
            if (checkFileResult != "")
            {
                bindingContext.ModelState.AddModelError("ImageContentType", checkFileResult);
            }

            user.ImageBase64 = GetByteArrayString(file);
            user.ImageContentType = file.ContentType;

            return user;

        }

        private string GetByteArrayString(HttpPostedFileBase file)
        {
            byte[] data;

            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                data = binaryReader.ReadBytes(file.ContentLength);
            }

            return Convert.ToBase64String(data);
        }
        private string CheckFile(HttpPostedFileBase file)
        {
            string result = "";

            //file exist
            if (file == null)
            {
                result = "Upload the file!";
            }

            //file format
            if (file.ContentType != "image/png")
            {
                result = "Image only .png!";
            }

            //file length
            if (file.ContentLength >= 10485760)
            {
                result = "Less 10 Mb !";
            }

            return result;
        }
    }
}