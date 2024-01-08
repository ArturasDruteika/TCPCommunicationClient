using Client.MessageHandlers.MessageReceivers;
using Client.MessageHandlers.MessageSenders;
using Client.ImageSenders;
using Client.AsyncTCPClients;
using System.Reflection;

namespace Client.ClientRunners
{
    public class ClientRunner
    {
        const string IP = "127.0.0.1";
        const int PORT = 1024;

        public ClientRunner() 
        {

        }

        public void Run() 
        {
            ImageSender imageSender = new ImageSender();
            MessageSender messageSender = new MessageSender();
            MessageReceiver messageReceiver = new MessageReceiver();

            string executablePath = Assembly.GetExecutingAssembly().Location;
            string? executableDirectory = Path.GetDirectoryName(executablePath);
            string imgPath = Path.Combine(executableDirectory, "data", "images", "fish.jpg");

            RunClient(messageSender, imageSender, messageReceiver);
        }

        private static void RunClient(IMessageSender messageSender, IImageSender imageSender, IMessageReceiver messageReceiver)
        {
            Console.WriteLine("Enter username: ");
            string? username = Console.ReadLine();

            AsyncTCPClient client = new AsyncTCPClient(IP, PORT, username, messageSender, imageSender, messageReceiver);
            client.Run();
        }

    }
}
