using ImageMagick;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

internal class TiffImageConverter : IImageConverter
{
    private readonly MagickReadSettings _readTiffSettings = new()
    {
        Compression = CompressionMethod.LZW
    };

    public string FileFormat => Constants.IO.TIFF;

    public MagickFormat MagickFileFormat => MagickFormat.Tiff;

    public int DisplayingOrder => Constants.IO.TIFF_ORDER;

    public byte[] Convert(byte[] source)
        => new MagickImage(source, _readTiffSettings).ToByteArray(MagickFileFormat);
}
