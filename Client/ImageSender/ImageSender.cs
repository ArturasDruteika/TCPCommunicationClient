using ClientWrapper;
using System.Net.Sockets;
using System.Text;


namespace Client.ImageSender
{
    public class ImageSender : IImageSender
    {
        public async Task SendImg(string imgPath, NetworkStream stream)
        {
            byte[] header = Encoding.ASCII.GetBytes(CommandTypes.IMG);
            await stream.WriteAsync(header, 0, header.Length);
            using var fileStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
            await fileStream.CopyToAsync(stream);
        }
    }
}
