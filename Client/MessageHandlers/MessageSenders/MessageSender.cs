using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Client.MessageHandlers.MessageSenders
{
    public class MessageSender : IMessageSender
    {
        public async Task SendMsg(string msg, NetworkStream stream, CancellationToken cancellationToken)
        {
            byte[] header = Encoding.ASCII.GetBytes(CommandTypes.MSG);
            byte[] data = Encoding.ASCII.GetBytes(msg);
        }
    }
}
