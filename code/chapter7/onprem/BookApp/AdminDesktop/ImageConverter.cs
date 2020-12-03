using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AdminDesktop
{
    public class ImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                byte[] byteArray = (byte[])value;

                if (byteArray == null)
                    return null;
                BitmapImage image = new BitmapImage();
                using (MemoryStream imageStream = new MemoryStream())
                {
                    imageStream.Write(byteArray, 0, byteArray.Length);
                    imageStream.Seek(0, SeekOrigin.Begin);
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = imageStream;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
            }
            return null;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
