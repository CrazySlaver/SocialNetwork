using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using MusicWave.Helpers;
using MusicWave.Models;

namespace MusicWave.ConnectToDB
{
    public static class DataBase
    {
       public static User GetUser(string email, string password)
        {
            var entity = new WorldDBEntities();

            return entity.User.First(u => u.Email == email && u.Password == password);
        }

        public static User GetUser(string email)
        {
            var entity = new WorldDBEntities();

            return entity.User.First(u => u.Email == email);
        }

        public static User[] GetAllUsers()
        {
            var entity = new WorldDBEntities();

            return entity.User.ToArray();
        }

        public static User GetUserByCookeis(string coockies)
        {
            var entity = new WorldDBEntities();

            return entity.User.First(u => u.Cookies == coockies);
        }

        private static bool CheckSex(string sex)
        {
            bool result = sex == "Male";
            if (sex == "Female")
            {
                result = false;
            }
            return result;

        }
        public static void AddUser(CustomUser model)
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
                    Password = SecurityHelper.Hash(model.Password),
                    Sex = CheckSex(model.Sex),
                    ImageBase64 = model.ImageBase64,
                    ImageContentType = model.ImageContentType,

                    Cookies = Guid.NewGuid().ToString()
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