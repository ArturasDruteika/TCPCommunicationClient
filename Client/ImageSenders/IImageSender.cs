using System.Net.Sockets;

namespace Client.ImageSenders
{
    public interface IImageSender
    {
        public Task SendImg(string imgPath, NetworkStream stream, CancellationToken cancellationToken);
    }
}
