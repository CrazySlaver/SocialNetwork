using System;
using Microsoft.AspNet.SignalR;

namespace MusicWave.Areas.Chat.Hubs
{
    public class SignalRChatHub : Hub
    {
        public void BroadCastMessage(String msgFrom, String msg)
        {
            Clients.All.receiveMessage(msgFrom, msg);
        }
    }
}