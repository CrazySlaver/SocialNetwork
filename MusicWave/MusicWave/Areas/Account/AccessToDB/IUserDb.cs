using MusicWave.Models;

namespace MusicWave.Areas.Account.AccessToDB
{
    public interface IUserDb
    {
        void AddUserToDb(CustomUser model);
        bool CheckEmail(string email);
    }
}
