using System.IO;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

internal interface IBmpImageConverter : IImageConverter
{
    string IImageConverter.Write(byte[] source, string filePath)
    {
        var path = Path.GetFileNameWithoutExtension(filePath) + "1" + Path.GetExtension(filePath);
        File.WriteAllBytes(path, source);
        return path;
    }
}
