using System;
using System.Linq;
using System.Reflection;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;

public static class ConvertersFactory
{
    private static readonly IImageConverter?[] _converters;
    private static readonly string[] _acceptableFormats;

    static ConvertersFactory()
    {
        _converters = Assembly.GetAssembly(typeof(IImageConverter))?.GetTypes()
                              .Where(type => type.IsAbstract == false && type.GetInterface(nameof(IImageConverter)) is not null)
                              .Select(type => Activator.CreateInstance(type) as IImageConverter)
                              .OrderBy(converter => converter?.DisplayingOrder)
                              .ToArray() ?? Array.Empty<IImageConverter>();

        _acceptableFormats = _converters.Where(converter => converter?.DisplayingOrder != Constants.IO.NONE)
                                        .Select(converter => converter?.FileFormat ?? string.Empty)
                                        .Where(filePath => string.IsNullOrEmpty(filePath) == false)
                                        .ToArray();
    }

    public static TConverter? GetConverter<TConverter>()
        where TConverter : IImageConverter
        => (TConverter?)_converters.FirstOrDefault(converter => converter is TConverter);

    public static IImageConverter? GetConverter(string fileFormat)
        => _converters.FirstOrDefault(converter => converter?.FileFormat.Equals(fileFormat) ?? false);

    public static bool IsAcceptableFormat(string currentFormat)
        => _acceptableFormats.Contains(currentFormat);

    public static string[] AcceptableFormats
        => _acceptableFormats;

}
