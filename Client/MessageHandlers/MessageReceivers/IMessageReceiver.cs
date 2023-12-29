using System.Net.Sockets;

namespace Client.MessageHandlers.MessageReceivers
{
    public interface IMessageReceiver
    {
        public Task ReceiveMsg(NetworkStream stream, CancellationToken cancellationToken);
    }
}
