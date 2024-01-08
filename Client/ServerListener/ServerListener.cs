using Client.MessageHandlers.MessageReceivers;
using System.Net.Sockets;

namespace Client.ServerListeners
{
    public class ServerListener
    {
        private NetworkStream Stream;
        private readonly CancellationTokenSource Cts;
        private IMessageReceiver MessageReceiver;
        private Task? ListeningTask;

        public ServerListener(NetworkStream stream, IMessageReceiver messageReceiver)
        {
            Stream = stream;
            MessageReceiver = messageReceiver;
            Cts = new CancellationTokenSource();
        }

        public void StartListening()
        {
            ListeningTask = Task.Run(ListenForMessages, Cts.Token);
        }

        public void StopListening()
        {
            Cts.Cancel();
            ListeningTask?.Wait(); // Optionally wait for the task to complete
        }

        private void ListenForMessages()
        {
            MessageReceiver.ReceiveMsg(Stream, Cts.Token);
        }
    }
}
