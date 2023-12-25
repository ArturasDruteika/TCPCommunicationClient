using ClientWrapper;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientWrapper
{
    class Client
    {
        private string _ip;
        private int _port;
        private TcpClient _client;
        private NetworkStream _stream;

        public Client(string ip, int port)
        {
            _ip = ip;
            _port = port;
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

        public async Task SendMessageAsync(string message)
        {
            byte[] header = Encoding.ASCII.GetBytes(CommandTypes.MSG);
            byte[] data = Encoding.ASCII.GetBytes(message);

            await _stream.WriteAsync(header, 0, header.Length);
            await _stream.WriteAsync(data, 0, data.Length);
        }

        public async Task SendImgAsync(string imgPath)
        {
            byte[] header = Encoding.ASCII.GetBytes(CommandTypes.IMG);

            await _stream.WriteAsync(header, 0, header.Length);

            using var fileStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            await fileStream.CopyToAsync(_stream);
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
