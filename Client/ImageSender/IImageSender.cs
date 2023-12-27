using System.Net.Sockets;

namespace Client.ImageSender
{
    public interface IImageSender
    {
        public void SendImg(string imgPath, NetworkStream stream);
    }
}
