using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Helpers;
using MusicWave.Models;

namespace MusicWave.Areas.Account.AccessToDB
{
    public class UserDb : IUserDb
    {
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

        public bool CheckEmail(string email)
        {
            User emailDb;
            bool flag = false;
            using (var db = new PeopleDBEntities())
            {
                emailDb = db.User.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            }
            if (emailDb == null)
            {
                flag = true;
            }
            return flag;
        }

        //public IEquatable<CustomUser> GetAllFriends()
        //{

        //}

        //TODO life dropdown list
        public IEnumerable<User> GetSeekingUser(string name)
        {
            IEnumerable<User> usersList;

            using (var entity = new PeopleDBEntities())
            {
                usersList = (from users in entity.User
                             where (users.FirstName + users.LastName).Contains(name)
                             select users).ToList();

            }
            return usersList;
        }

        public bool AddUserToFriend(Guid userId, Guid friendId)
        {
            bool result = false;
            using (var db = new PeopleDBEntities())
            {

                var check = (from friend in db.FriendRelationship
                            .Where(f => (f.FriendId == friendId && f.UserId == userId) || (f.FriendId == userId && f.UserId == friendId))
                            select friend).ToList();
                if (check.Count == 0)
                {
                    var entity1 = new FriendRelationship
                    {
                        Id = Guid.NewGuid(),
                        FriendId = friendId,
                        UserId = userId,
                        status = false
                    };
                    var entity2 = new FriendRelationship
                    {
                        Id = Guid.NewGuid(),
                        FriendId = userId,
                        UserId = friendId,
                        status = false
                    };
                    try
                    {

                        db.FriendRelationship.Add(entity1);
                        db.FriendRelationship.Add(entity2);
                        db.SaveChanges();
                        result = true;
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
            return result;
        }
    }
}