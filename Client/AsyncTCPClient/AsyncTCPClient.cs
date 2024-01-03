using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Client.ImageSender;
using Client.MessageHandlers.MessageSenders;
using Client.MessageHandlers.MessageReceivers;
using System.Threading;


namespace Client.AsyncTCPClient
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

            _ = SendInitialMsgAsync();
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
        }

        public async Task SendMsgAsync(string msg)
        {
            await MessageSender.SendMsg(msg, Stream, Cts.Token);
        }

        public async Task SendImgAsync(string imgPath)
        {
            await ImageSender.SendImg(imgPath, Stream, Cts.Token);
        }

        public async Task ReceiveMsg()
        {
            await MessageReceiver.ReceiveMsg(Stream, Cts.Token);
        }

        private async Task SendInitialMsgAsync()
        {
            await SendMsgAsync(Username);
        }
    }
}
