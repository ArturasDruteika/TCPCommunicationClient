using Client.AsyncTCPClient;
using Client.ImageSender;
using Client.MessageHandlers.MessageReceivers;
using Client.MessageHandlers.MessageSenders;
using System.Reflection;

class Program
{
    const string IP = "127.0.0.1";
    const int PORT = 1024;

    static void Main(string[] args)
    {
        DoSingleClientConnection();
    }

    private async static void DoSingleClientConnection()
    {
        ImageSender imageSender = new ImageSender();
        MessageSender messageSender = new MessageSender();
        MessageReceiver messageReceiver = new MessageReceiver();

        string executablePath = Assembly.GetExecutingAssembly().Location;
        string executableDirectory = Path.GetDirectoryName(executablePath);
        string imgPath = Path.Combine(executableDirectory, "data", "images", "fish.jpg");

        AsyncTCPClient client = new AsyncTCPClient(IP, PORT, messageSender, imageSender, messageReceiver);
        await client.SendImgAsync(imgPath);

        client.CloseConnection();
    }
}
