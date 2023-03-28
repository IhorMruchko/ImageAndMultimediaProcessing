using ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class BitmapExtension
{
    public static BitmapImage LoadImage(this BitmapImage image, string source)
    {
        image.BeginInit();
        image.UriSource = new Uri(source);
        image.EndInit();

        return image;
    }

    public static (Bitmap R, Bitmap G, Bitmap B, Bitmap Image, ChanallizedImage Results) Substract(this Bitmap firstImage, Bitmap secondImage)
    {
        var imageAsChannels = new ChanallizedImage(firstImage.Width, firstImage.Height);

        for (var x = 0; x < firstImage.Width; ++x)
            for (var y = 0; y < firstImage.Height; ++y)
                imageAsChannels.SubstractColors(firstImage.GetPixel(x, y), secondImage.GetPixel(x, y), x, y);

        var (redImage, greenImage, blueImage) = (new Bitmap(firstImage), new Bitmap(firstImage), new Bitmap(firstImage));

        for (var x = 0; x < firstImage.Width; ++x)
        {
            for (var y = 0; y < firstImage.Height; ++y)
            {
                redImage.SetPixel(x, y, imageAsChannels.GetColor(ImageChannels.Red, x, y));
                blueImage.SetPixel(x, y, imageAsChannels.GetColor(ImageChannels.Green, x, y));
                greenImage.SetPixel(x, y, imageAsChannels.GetColor(ImageChannels.Blue, x, y));
                firstImage.SetPixel(x, y, imageAsChannels.GetColor(ImageChannels.All, x, y));
            }
        }

        return (redImage, greenImage, blueImage, firstImage, imageAsChannels);
    }
}
