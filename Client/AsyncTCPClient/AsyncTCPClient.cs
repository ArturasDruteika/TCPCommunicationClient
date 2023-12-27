using ClientWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Client.MessageSender;
using Client.ImageSender;


namespace Client.AsyncTCPClient
{
    public class AsyncTCPClient
    {
        private string _ip;
        private int _port;
        private TcpClient _client;
        private NetworkStream _stream;
        private IMessageSender _messageSender;
        private IImageSender _imageSender;

        public AsyncTCPClient(string ip, int port, IMessageSender messageSender, IImageSender imageSender)
        {
            _ip = ip;
            _port = port;
            _messageSender = messageSender;
            _imageSender = imageSender;

            _client = new TcpClient(_ip, _port);
            _stream = _client.GetStream();
        }

        public void Close()
        {
            _client.Close();
        }

        public void CloseStream()
        {
            _stream.Close();
        }

        public async Task SendMsgAsync(string msg)
        {
            await _messageSender.SendMsg(msg, _stream);
        }

        public async Task SendImgAsync(string imgPath)
        {
            await _imageSender.SendImg(imgPath, _stream);
        }

        public void ReceiveMsg()
        {
            byte[] data = new byte[1024];
            String responseData = String.Empty;

            int bytes = _stream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: " + responseData);
        }
    }
}
