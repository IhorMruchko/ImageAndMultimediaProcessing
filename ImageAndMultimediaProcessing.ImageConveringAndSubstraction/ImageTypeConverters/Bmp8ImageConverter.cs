using ImageAndMultimediaProcessing.ImageConveringAndSubstraction.Extensions;
using ImageMagick;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

internal class Bmp8ImageConverter : IImageConverter
{
    private static readonly MagickReadSettings _convertionSettings = new()
    {
        Compression = CompressionMethod.RLE
    };

    private static readonly QuantizeSettings _quantizeSettings = new()
    {
        Colors = 256
    };

    public string FileFormat => Constants.IO.BMP;

    public MagickFormat MagickFileFormat => MagickFormat.Bmp;

    public int DisplayingOrder => Constants.IO.BMP_ORDER;

    public byte[] Convert(byte[] source)
        => new MagickImage(source.Quantize(_quantizeSettings), _convertionSettings)
                          .ToByteArray(MagickFileFormat);
}
