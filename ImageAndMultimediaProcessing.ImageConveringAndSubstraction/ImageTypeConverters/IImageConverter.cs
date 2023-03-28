using ImageMagick;
using System.IO;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

public interface IImageConverter
{
    string FileFormat { get; }

    MagickFormat MagickFileFormat { get; }

    int DisplayingOrder { get; }

    byte[] Convert(byte[] source);

    MagickImage Decode(byte[] source)
        => new(source);

    byte[] Read(string filePath)
        => File.ReadAllBytes(filePath);

    string Write(byte[] source, string filePath)
    {
        File.WriteAllBytes(filePath, source);
        return filePath;
    }
}
