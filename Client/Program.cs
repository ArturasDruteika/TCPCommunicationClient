using ClientWrapper;
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
        string executablePath = Assembly.GetExecutingAssembly().Location;
        string executableDirectory = Path.GetDirectoryName(executablePath);
        string imgPath = Path.Combine(executableDirectory, "data", "images", "fish.jpg");

        Client client = new Client(IP, PORT);
        await client.SendImgAsync(imgPath);

        client.Close();
        client.CloseStream();
    }
}
