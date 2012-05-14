using PlayR.Core;
using SignalR.Hubs;

namespace PlayR.Hubs
{
    public class Chat : Hub
    {
        readonly IMessageLogger _messageLogger;

        public Chat(IMessageLogger messageLogger)
        {
            _messageLogger = messageLogger;
        }

        public void Send(string message, string user)
        {
            // Call the addMessage method on all clients
            Clients.addMessage(message, user);
            _messageLogger.LogMessage(message, user);
        }

    }
}