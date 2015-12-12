using System;
using System.Linq;
using System.Web.Security;
using MusicWave.Models;

namespace MusicWave.Providers
{
    public class MusicWaveRoleProviders : RoleProvider
    {
        public override string[] GetRolesForUser(string email)
        {
            string[] role = new string[] { };
            using (PeopleDBEntities _db = new PeopleDBEntities())
            {
                try
                {
                    // Получаем пользователя
                    User user = (from u in _db.User
                                 where u.Email == email
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        var userRole = _db.Role.Find(user.RoleId);

                        if (userRole != null)
                        {
                            role = new string[] { userRole.Name };
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            }
            return role;
        }
        public override bool IsUserInRole(string email, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (PeopleDBEntities _db = new PeopleDBEntities())
            {
                try
                {
                    // Получаем пользователя
                    User user = (from u in _db.User
                                 where u.Email == email
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // получаем роль
                        var userRole = _db.Role.Find(user.RoleId);

                        //сравниваем
                        if (userRole != null && userRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }

        #region Not implement
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}