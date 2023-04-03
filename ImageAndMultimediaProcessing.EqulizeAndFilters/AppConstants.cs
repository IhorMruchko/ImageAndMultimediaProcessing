using System;
using System.IO;

namespace ImageAndMultimediaProcessing.EqulizeAndFilters;

public static class AppConstants
{
    public const string RESOURCES_DIRECTORY = "E:\\Programming\\C#\\ImageAndMultimediaProcessing\\ImageAndMultimediaProcessing\\ImageAndMultimediaProcessing.EqulizeAndFilters\\Resources\\Images\\BmpImages\\";
    public const string HISTOGRAM_DIRECTORY = "E:\\Programming\\C#\\ImageAndMultimediaProcessing\\ImageAndMultimediaProcessing\\ImageAndMultimediaProcessing.EqulizeAndFilters\\Resources\\Images\\Histograms\\";
    public const string BMP_FILTER = "BMP|*.bmp";
    public static string CurrentDirectory 
        => Directory.Exists(RESOURCES_DIRECTORY)
        ? RESOURCES_DIRECTORY
        : Environment.CurrentDirectory;

    public static string HistogramDirectory
        => Directory.Exists(HISTOGRAM_DIRECTORY)
        ? HISTOGRAM_DIRECTORY
        : Environment.CurrentDirectory;
}
