using SignalR.Hubs;

namespace PlayR.Hubs
{
    public class Chat : Hub
    {
        public void Send(string message, string user)
        {
            // Call the addMessage method on all clients
            Clients.addMessage(message, user);
        }
    }
}