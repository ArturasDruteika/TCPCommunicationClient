using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ImageSender
{
    public interface IImageSender
    {
        public void SendImg(string imgPath);
    }
}
