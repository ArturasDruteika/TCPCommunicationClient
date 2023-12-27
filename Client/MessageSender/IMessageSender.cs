using System.IO;
using System.Net.Sockets;

namespace Client.MessageSender
{
    public interface IMessageSender
    {
        public Task SendMsg(string msg, NetworkStream stream);
    }
}
