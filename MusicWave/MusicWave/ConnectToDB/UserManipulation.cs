using System;
using System.Web.Mvc;
using MusicWave.Helpers;
using MusicWave.Models;

namespace MusicWave.ConnectToDB
{
    public class UserManipulation
    {
        public void AddUserToDb([ModelBinder(typeof(UserModelBinder))]CustomUser model)
        {
            using (var db = new WorldDBEntities())
            {
                var entity = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    City = model.City,
                    Description = model.Description,
                    Email = model.Email,
                    Password = model.Password,
                    Sex = model.Sex,
                    ImageBase64 = model.ImageBase64,
                    ImageContentType = model.ImageContentType
                };
                db.User.Add(entity);
                db.SaveChanges();
            }
        }
    }
}