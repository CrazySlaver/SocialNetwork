using System.Collections.Generic;
using MusicWave.Models;

namespace MusicWave.Areas.Account.AccessToDB
{
    public interface IUserDb
    {
        void AddUserToDb(CustomUser model);
        bool CheckEmail(string email);
        IEnumerable<User> GetSeekingUser(string name);
    }
}
