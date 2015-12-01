using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using MusicWave.Models;

namespace MusicWave.Areas.Account.Helpers
{
    public class UserModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var user = base.BindModel(controllerContext, bindingContext) as CustomUser;
           
            HttpRequestBase request = controllerContext.HttpContext.Request;
            HttpFileCollectionBase files = request.Files;

            HttpPostedFileBase file = files.Get(0);
            if (file.ContentLength != 0)
            {
                string checkFileResult = CheckFile(file);
                if (checkFileResult == "")
                {
                    user.ImageBase64 = GetByteArrayString(file);
                    user.ImageContentType = file.ContentType;
                }
                else
                {
                    bindingContext.ModelState.AddModelError("ImageContentType", checkFileResult);
                }
                

            }
            else
            {
                //TODO прописать относительный путь
                FileStream fs = new FileStream(@"D:\ProjectX\SocialNetwork\MusicWave\MusicWave\Content\defaultAvatar.png", FileMode.Open, FileAccess.Read);

                // Create a byte array of file stream length
                byte[] imageData = new byte[fs.Length];

                //Read block of bytes from stream into the byte array
                fs.Read(imageData, 0, Convert.ToInt32(fs.Length));

                //Close the File Stream
                fs.Close();
                //return the byte data

                user.ImageBase64 = Convert.ToBase64String(imageData); ;
                user.ImageContentType = "image/png";
            }
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

            //file format
            if ((file.ContentType != "image/png") && (file.ContentType != "image/jpeg"))
            {
                result = "Image must be png or jpg";
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