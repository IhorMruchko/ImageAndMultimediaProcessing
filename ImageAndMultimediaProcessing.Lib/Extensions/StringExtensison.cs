using ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;
using System.Drawing;
using System.IO;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

/// <summary>
/// Extension class for the <see cref="string"/> objects.
/// </summary>
public static class StringExtension
{
    private const string PATH_FORMAT = "_from_{0}_{1}{2}";
    /// <summary>
    /// Simplifies <see cref="string"/> formatting.
    /// </summary>
    /// <param name="format">Format of the <see cref="string"/>.</param>
    /// <param name="args">Values to put in the format of the <see cref="string"/>.</param>
    /// <returns>Formatted <see cref="string"/>.</returns>
    public static string Format(this string format, params object[] args)
        => string.Format(format, args);

    public static Bitmap? ToBitmap(this string filePath)
    {
        if (File.Exists(filePath) == false) return null;
        return new Bitmap(filePath);
    }

    public static string FormPath(this string directory, string filePath, ImageChannels chanel, string extension)
        => Path.Combine(directory,
                        Path.GetFileNameWithoutExtension(filePath)
            + PATH_FORMAT.Format(extension[1..],
                                 chanel,
                                 Path.GetExtension(filePath)));
}