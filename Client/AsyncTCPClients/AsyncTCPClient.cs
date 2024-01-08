using System.Net.Sockets;
using Client.ImageSenders;
using Client.MessageHandlers.MessageSenders;
using Client.MessageHandlers.MessageReceivers;
using Client.ServerListeners;
using Client.TextInputHandlers;

namespace Client.AsyncTCPClients
{
    public class AsyncTCPClient
    {
        private string Ip;
        private int Port;
        private string Username;
        private TcpClient? Client;
        private NetworkStream? Stream;
        private IMessageSender MessageSender;
        private IImageSender ImageSender;
        private IMessageReceiver MessageReceiver;
        private CancellationTokenSource Cts = new CancellationTokenSource();

        private ServerListener ServerListenerObj;
        private TextInputHandler TextInputHandlerObj;

        public AsyncTCPClient(string ip, int port, string username, IMessageSender messageSender, IImageSender imageSender, IMessageReceiver messageReceiver)
        {
            Ip = ip;
            Port = port;
            Username = username;
            MessageSender = messageSender;
            ImageSender = imageSender;
            MessageReceiver = messageReceiver;

            Client = new TcpClient(Ip, Port);
            Stream = Client.GetStream();
            ServerListenerObj = new ServerListener(Stream, messageReceiver);
            TextInputHandlerObj = new TextInputHandler(Stream, messageSender);

            SendInitialMsgAsync();
        }

        public void CloseConnection()
        {
            if (Stream != null)
            {
                Stream.Close(); // Close the stream first
                Stream = null;
            }

            if (Client != null)
            {
                Client.Close(); // Then close the client
                Client = null;
            }

            Cts.Cancel();
            ServerListenerObj.StopListening();
        }

        public void Run()
        {
            StartListening();
            StartUserInputWaiting();

            while (!Cts.Token.IsCancellationRequested)
            {
                if (Cts.Token.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private void StartListening()
        {
            if (Stream == null)
            {
                return;
            }

            ServerListenerObj.StartListening();
        }

        private void StartUserInputWaiting()
        {
            if (Stream == null)
            {
                return;
            }

            TextInputHandlerObj.StartListening();
        }

        private void SendInitialMsgAsync()
        {
            MessageSender.SendMsg(Username, Stream);
        }
    }
}
