using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;
using MusicWave.Models;

namespace MusicWave.Areas.Account.AccessToDB
{
    public class UserManipulation
    {
        //TODO проверять адрес на существование в базе
        //TODO коректные сообщения обшибки (Error Class) возможно реализовать ErrorFilter
        public bool CheckGender(string gender)
        {
            bool result = gender == "Male";
            if (gender == "Female")
            {
                result = false;
            }
            return result;

        }

        public Guid GetRole(string role)
        {
            var roleId = new Guid();
            using (var db = new PeopleDBEntities())
            {
                ((IObjectContextAdapter)db).ObjectContext.CommandTimeout = 180;
                try
                {
                    var user = db.Role.FirstOrDefault(u => u.Name == role);
                    if (user != null)
                    {
                        roleId = user.RoleId;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return roleId;
        }

        public void AddUserToDb(CustomUser model)
        {
            using (var db = new PeopleDBEntities())
            {
                var entity = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    City = model.City,
                    AboutUser = model.Description,
                    Email = model.Email,
                    Password = Crypto.SHA256(model.Password),
                    Gender = CheckGender(model.Sex),
                    ImageBase64 = model.ImageBase64,
                    ImageContentType = model.ImageContentType,
                    RoleId = GetRole("user")
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