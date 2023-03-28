using ImageMagick;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

internal class JpegImageConverter : IImageConverter
{
    private readonly MagickReadSettings _jpegReadSettings = new()
    {
        Compression = CompressionMethod.JPEG
    };

    public string FileFormat => Constants.IO.JPEG;

    public MagickFormat MagickFileFormat => MagickFormat.Jpeg;

    public int DisplayingOrder => Constants.IO.JPEG_ORDER;

    public byte[] Convert(byte[] source)
        => new MagickImage(source, _jpegReadSettings).ToByteArray(MagickFileFormat);
}
