using System;
using System.Data.Entity.Validation;
using System.Web.Helpers;
using MusicWave.Models;

namespace MusicWave.ConnectToDB
{
    public class UserManipulation
    {
        private bool CheckSex(string sex)
        {
            bool result = sex == "Male";
            if (sex == "Female")
            {
                result = false;
            }
            return result;

        }

        public void AddUserToDb(CustomUser model)
        {
            using (var db = new WorldDBEntities1())
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
                    Password = Crypto.HashPassword(model.Password),
                    Sex = CheckSex(model.Sex),
                    ImageBase64 = model.ImageBase64,
                    ImageContentType = model.ImageContentType
                };
                try
                {

                    db.User.Add(entity);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }
    }
}