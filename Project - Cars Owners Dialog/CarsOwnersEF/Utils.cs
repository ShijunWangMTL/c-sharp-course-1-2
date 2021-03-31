using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CarsOwnersEF
{
    static class Utils
    {
        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }



        /* public static byte[] ConvertToBytes(BitmapImage bitmapImage)
         {
             using (var stream = new MemoryStream())
             {
                 var encoder = new PngBitmapEncoder(); // or some other encoder
                 encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                 encoder.Save(stream);
                 return stream.ToArray();
             }
         }*/
    }
}
