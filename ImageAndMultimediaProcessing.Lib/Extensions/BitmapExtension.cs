using ImageAndMultimediaProcessing.Lib.Entities;
using ImageAndMultimediaProcessing.Lib.Entities.Enums;
using ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using static System.Math;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class BitmapExtension
{
    private const int COLOR_RANGE = 256;
    private const int COLOR_MODIFIER = 255;
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

    public static Bitmap Equalize(this Bitmap image)
    {
        var (redPixelsCount, greenPixelsCount, bluePixelsCount) = image.CountChanels();
        var result = new Bitmap(image.Width, image.Height);
        
        for (var i = 0; i < result.Width; ++i)
        {
            for (var j = 0; j < result.Height; ++j)
            {
                var currentPixel = image.GetPixel(i, j);
                result.SetPixel(i, j, 
                    Color.FromArgb((int)(redPixelsCount[currentPixel.R] * COLOR_MODIFIER),
                                   (int)(greenPixelsCount[currentPixel.G] * COLOR_MODIFIER),
                                   (int)(bluePixelsCount[currentPixel.B] * COLOR_MODIFIER)));
            }
        }
       
        return result;
    }

    public static (double[] r, double[] g, double[] b) CountChanels(this Bitmap image)
    {
        var (r, g, b) = (new double[COLOR_RANGE], new double[COLOR_RANGE], new double[COLOR_RANGE]);
        var totalPixelsAmount = image.Width * image.Height;
        
        for (var i = 0; i < image.Width; ++i)
        {
            for (var j = 0; j < image.Height; ++j)
            {
                var currentPixel = image.GetPixel(i, j);
                r[currentPixel.R] += 1d / totalPixelsAmount;
                g[currentPixel.G] += 1d / totalPixelsAmount ;
                b[currentPixel.B] += 1d / totalPixelsAmount;
            }
        }

        for (var i = 1; i < COLOR_RANGE; ++i)
        {
            r[i] = r[i - 1] + r[i];
            g[i] = g[i - 1] + g[i];
            b[i] = b[i - 1] + b[i];
        }
        return (r, g, b);
    }

    public static HistogramContainer CreateHistogram(this Bitmap image) 
        => new HistogramContainer().BuildHistogram(image);

    public static Bitmap ApplyFilter(this Bitmap image, Filters filter)
    {
        switch (filter)
        {
            case Filters.Roberts:
                return image.ApplyFilter(Constants.HorizontalRobertsOperator, Constants.VerticalRobertsOperator);
            case Filters.Prewitt:
                return image.ApplyFilter(Constants.HorizontalPrewittOperator, Constants.VerticalPrewittOperator);
            case Filters.Sobel:
                return image.ApplyFilter(Constants.HorizontalSobelOperator, Constants.VerticalSobelOperator);
            default:
                return image;
        }
    }

    public static Bitmap ApplyFilter(this Bitmap image,
        Matrix<int> horizontalOperator,
        Matrix<int> verticalOperator)
    {
        var result = new Bitmap(image.Width, image.Height);
        var pixelStorage = new double[image.Width, image.Height];
        var min = double.MaxValue;
        var max = double.MinValue;
        for (var x = 1; x < image.Width - 1; ++x)
        {
            for (var y = 1; y < image.Height - 1; ++y)
            {
                var pixelMatrix = image.GetPixelMatrix(x, y, horizontalOperator.Height);
                var filteredPixel = pixelMatrix.ScalarDistance(horizontalOperator, verticalOperator);
                pixelStorage[x, y-1] = filteredPixel;
                
                if (pixelStorage[x, y-1] < min) min = pixelStorage[x, y - 1];
                if (pixelStorage[x, y - 1] > max) max = pixelStorage[x, y - 1];
            }
        }

        for (var x = 1; x < image.Width - 1; ++x)
        {
            for (var y = 1; y < image.Height - 2; ++y)
            {
                var normilizedColor = pixelStorage[x, y].Normalize(min, max, COLOR_MODIFIER);
                result.SetPixel(x, y, Color.FromArgb(normilizedColor, normilizedColor, normilizedColor));
            }
        }

        return result;
    }

    public static Matrix<Color> GetPixelMatrix(this Bitmap image, int x, int y, int size)
    {
        return size == 2 ? image.GetTwoMatrix(x, y) : size == 3 ? image.GetThreeMatrix(x, y) : Matrix<Color>.Empty;
    }

    public static Matrix<Color> GetTwoMatrix(this Bitmap image, int x, int y)
    {
        return new Color[,]
        {
            { image.GetPixel(x-1, y-1), image.GetPixel(x, y-1)},
            { image.GetPixel(x-1, y), image.GetPixel(x, y) }
        };
    }

    public static Matrix<Color> GetThreeMatrix(this Bitmap image, int x, int y)
    {
        return new Color[,]
        {
            { image.GetPixel(x-1, y-1), image.GetPixel(x, y-1), image.GetPixel(x+1, y-1)},
            { image.GetPixel(x-1, y), image.GetPixel(x, y), image.GetPixel(x+1, y) },
            { image.GetPixel(x-1, y+1), image.GetPixel(x, y+1), image.GetPixel(x+1, y+1) }
        };
    }
}
