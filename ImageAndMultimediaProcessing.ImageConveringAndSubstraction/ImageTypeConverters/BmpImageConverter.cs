using ImageMagick;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

internal class BmpImageConverter : IBmpImageConverter
{
    public string FileFormat => Constants.IO.BMP_PURE;

    public MagickFormat MagickFileFormat => MagickFormat.Bmp;

    public int DisplayingOrder => Constants.IO.NONE;

    public byte[] Convert(byte[] source)
        => source;
}
