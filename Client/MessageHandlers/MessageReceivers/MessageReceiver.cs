using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.MessageHandlers.MessageReceivers
{
    public class MessageReceiver : IMessageReceiver
    {
        public async Task ReceiveMsg(NetworkStream stream)
        {
            byte[] data = new byte[1024];
            String responseData = String.Empty;

            int bytes = await stream.ReadAsync(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: " + responseData);
        }
    }
}
