using System.Net.Sockets;
using System.Text;
using Client.Containers;

namespace Client.MessageHandlers.MessageSenders
{
    public class MessageSender : IMessageSender
    {
        private int BytesForMsgLength = 16;

        public void SendMsg(string msg, NetworkStream stream)
        {
            byte[] header = Encoding.ASCII.GetBytes(CommandTypes.MSG);
            byte[] msgLen = Encoding.ASCII.GetBytes(FormMsgLenString(msg.Length));
            byte[] data = Encoding.ASCII.GetBytes(msg);

            // Create a new array that can hold both header and data
            byte[] combined = new byte[header.Length + msgLen.Length + data.Length];

            // Copy the header and data into the combined array
            Buffer.BlockCopy(header, 0, combined, 0, header.Length);
            Buffer.BlockCopy(msgLen, 0, combined, header.Length, msgLen.Length);
            Buffer.BlockCopy(data, 0, combined, header.Length + msgLen.Length, data.Length);

            // Send the combined message
            stream.Write(combined, 0, combined.Length);
        }

        private string FormMsgLenString(int msgLen)
        {
            string digits = msgLen.ToString();

            // Assign the digits to the first three bytes
            for (int i = digits.Length; i < BytesForMsgLength; i++)
            {
                digits += "_";
            }

            return digits;
        }
    }
}
