using Client.Containers;
using System.Net.Sockets;
using System.Text;


namespace Client.ImageSenders
{
    public class ImageSender : IImageSender
    {
        public async Task SendImg(string imgPath, NetworkStream stream, CancellationToken cancellationToken)
        {
            byte[] header = Encoding.ASCII.GetBytes(CommandTypes.IMG);
            await stream.WriteAsync(header, 0, header.Length, cancellationToken);
            using var fileStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            await fileStream.CopyToAsync(stream, cancellationToken);
        }
    }
}
