using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using MusicWave.Areas.UserProfile.Models;

namespace MusicWave
{
    //[Authorize]
    public class SignalRChatHub : Hub
    {
        private static List<ChatUser> _connectedUser = new List<ChatUser>();
        private static List<MessageDetail> _currentMessage = new List<MessageDetail>();

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (_connectedUser.Count(x => x.ConnectionId == id) == 0)
            {
                _connectedUser.Add(new ChatUser {ConnectionId = id, UserName = userName});

                Clients.Caller.onConnected(id, userName, _connectedUser, _currentMessage);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public void SendMessageToAll(string userName, string message)
        {
            AddMessageinCache(userName, message);   
            Clients.All.messageReceived(userName, message);
        }

        private void AddMessageinCache(string userName, string message)
        {
            _currentMessage.Add(new MessageDetail {UserName = userName, Message = message});

            if (_currentMessage.Count > 100)
                _currentMessage.RemoveAt(0);
        }

        public void SendPrivateMessage(string toUserId, string message)
        {
            string fromUserId = Context.ConnectionId;
            var toUser = _connectedUser.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = _connectedUser.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }
        }

        public override Task OnDisconnected(bool stopCalled = true)
        {
            var item =_connectedUser.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                _connectedUser.Remove(item);

                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.UserName);

            }

            return base.OnDisconnected(stopCalled);
        }
    }
}