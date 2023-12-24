using ClientWrapper;

class Program
{
    static void Main(string[] args)
    {
        const string IP = "127.0.0.1";
        const int PORT = 1024;
        Client client = new Client(IP, PORT);
        client.Run("Hello Server from Client!!!");
    }
}
