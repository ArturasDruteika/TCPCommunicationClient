using System;
using System.IO;
using System.Net.Sockets;
using System.Text;


namespace ClientWrapper
{
    class Client
    {
        private TcpClient _client;
        private string _ip;
        private int _port;
        private NetworkStream _stream;

        public Client(string ip, int port)
        {
            _ip = ip;
            _port = port;
            _client = new TcpClient(_ip, _port);
            _stream = _client.GetStream();
        }

        private void SendMsg(string msg)
        {
            // Encode a string into a byte array
            byte[] data = Encoding.ASCII.GetBytes(msg);

            // Send the message to the server
            _stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: " + msg);
        }

        private void ReceiveMsg()
        {
            // Buffer to store the response
            byte[] data = new byte[1024];
            String responseData = String.Empty;

            // Read the response from the server
            int bytes = _stream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: " + responseData);
        }

        public void Run(string msg)
        {
            SendMsg(msg);
            ReceiveMsg();

            _stream.Close();
            _client.Close();
        }
    }
}
