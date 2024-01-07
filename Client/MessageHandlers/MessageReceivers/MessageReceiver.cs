using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AsyncTcpServer.Containers;
using Client.Containers;

namespace Client.MessageHandlers.MessageReceivers
{
    public class MessageReceiver : IMessageReceiver
    {
        public async Task<ClientStatus> ReceiveMsg(NetworkStream stream, CancellationToken ctsToken)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, ctsToken)) != 0)
                {
                    KeyValuePair<string, string> receivedData = ProcessReceivedData(buffer, bytesRead);
                    Console.WriteLine($"{receivedData.Key}: " + receivedData.Value);
                    Array.Clear(buffer, 0, buffer.Length);
                }

                if (bytesRead == 0)
                {
                    return ClientStatus.DISCONNECTED;
                }

                return ClientStatus.CONNECTED;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return ClientStatus.DISCONNECTED;
            }
        }

        private static KeyValuePair<string, string> ProcessReceivedData(byte[] buffer, int bytesRead)
        {
            string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            if (receivedData.Length >= 3 && receivedData.Substring(0, 3) == CommandTypes.MSG)
            {
                receivedData = RemoveHeaderSubstr(receivedData);
                string sender = GetSenderName(receivedData);
                string message = RemoveSenderName(receivedData);
                KeyValuePair<string, string> processedData = new KeyValuePair<string, string>(sender, message);
                return processedData;
            }
            else
            {
                return new KeyValuePair<string, string>();
            }
            
        }

        private static string RemoveHeaderSubstr(string message)
        {
            return message[3..];
        }

        private static string GetSenderName(string message) 
        {
            string result = message.Substring(0, 16);
            result = result.TrimEnd();
            return result;
        }

        private static string RemoveSenderName(string receivedData)
        {
            return receivedData[16..];
        }
    }
}
