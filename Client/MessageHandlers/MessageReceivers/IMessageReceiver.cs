using System.Net.Sockets;

namespace Client.MessageHandlers.MessageReceivers
{
    public interface IMessageReceiver
    {
        public void ReceiveMsg(NetworkStream stream, CancellationToken ctsToken);
    }
}
