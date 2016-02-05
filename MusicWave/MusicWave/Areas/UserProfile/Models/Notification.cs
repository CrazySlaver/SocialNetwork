using System;

namespace MusicWave.Areas.UserProfile.Models
{
    public class Notification
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public bool Status { get; set; }
    }
}