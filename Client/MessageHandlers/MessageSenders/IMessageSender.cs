using System.Net.Sockets;

namespace Client.MessageHandlers.MessageSenders
{
    public interface IMessageSender
    {
        public void SendMsg(string msg, NetworkStream stream);
    }
}
