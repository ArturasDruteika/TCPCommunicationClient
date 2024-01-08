using System.Net.Sockets;
using System.Text;
using Client.Containers;

namespace Client.MessageHandlers.MessageReceivers
{
    public class MessageReceiver : IMessageReceiver
    {
        public void ReceiveMsg(NetworkStream stream, CancellationToken ctsToken)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while (!ctsToken.IsCancellationRequested)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);

                    KeyValuePair<string, string> receivedData = ProcessReceivedData(buffer, bytesRead);
                    Console.WriteLine($"{receivedData.Key}: " + receivedData.Value);
                    Array.Clear(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                Console.WriteLine("Exception: " + ex.Message);
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
