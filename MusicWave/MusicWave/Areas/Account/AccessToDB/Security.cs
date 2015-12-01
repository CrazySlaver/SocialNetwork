using System;
using System.Linq;
using System.Web.Helpers;
using MusicWave.Models;

namespace MusicWave.Areas.Account.AccessToDB
{
    public static class Security
    {
        public static User CheckPassword(string email, string password)
        {
            User user = null;
            using (var db = new WorldDBEntities2())
            {
                try
                {
                    user = db.User.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        var tempPassword = Crypto.SHA256(password);
                        if (user.Password == tempPassword)
                        {
                            return user;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}