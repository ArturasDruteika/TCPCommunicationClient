using AsyncTcpServer.Containers;
using System.Net.Sockets;

namespace Client.MessageHandlers.MessageReceivers
{
    public interface IMessageReceiver
    {
        public Task<ClientStatus> ReceiveMsg(NetworkStream stream, CancellationToken ctsToken);
    }
}
