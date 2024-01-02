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
        private string _ip;
        private int _port;
        private string _username;
        private TcpClient? _client;
        private NetworkStream? _stream;
        private IMessageSender _messageSender;
        private IImageSender _imageSender;
        private IMessageReceiver _messageReceiver;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public AsyncTCPClient(string ip, int port, string username, IMessageSender messageSender, IImageSender imageSender, IMessageReceiver messageReceiver)
        {
            _ip = ip;
            _port = port;
            _username = username;
            _messageSender = messageSender;
            _imageSender = imageSender;
            _messageReceiver = messageReceiver;

            _client = new TcpClient(_ip, _port);
            _stream = _client.GetStream();

            _ = SendInitialMsgAsync();
        }

        public void CloseConnection()
        {
            if (_stream != null)
            {
                _stream.Close(); // Close the stream first
                _stream = null;
            }

            if (_client != null)
            {
                _client.Close(); // Then close the client
                _client = null;
            }

            _cts.Cancel();
        }

        public async Task SendMsgAsync(string msg)
        {
            await _messageSender.SendMsg(msg, _stream, _cts.Token);
        }

        public async Task SendImgAsync(string imgPath)
        {
            await _imageSender.SendImg(imgPath, _stream, _cts.Token);
        }

        public async Task ReceiveMsg()
        {
            await _messageReceiver.ReceiveMsg(_stream, _cts.Token);
        }

        private async Task SendInitialMsgAsync()
        {
            await SendMsgAsync(_username);
        }
    }
}
