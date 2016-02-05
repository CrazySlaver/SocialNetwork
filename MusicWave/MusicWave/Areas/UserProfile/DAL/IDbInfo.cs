using System;
using System.Collections.Generic;
using MusicWave.Models;

namespace MusicWave.Areas.UserProfile.DAL
{
    public interface IDbInfo
    {
        IEnumerable<User> GetNotifications(Guid userId);
        void AcceptFriendship(Guid userId, Guid friendId);
        void RejectFriendship(Guid userId, Guid friendId);
    }
}