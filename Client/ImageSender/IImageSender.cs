using System.Net.Sockets;

namespace Client.ImageSender
{
    public interface IImageSender
    {
        public Task SendImg(string imgPath, NetworkStream stream, CancellationToken cancellationToken);
    }
}
