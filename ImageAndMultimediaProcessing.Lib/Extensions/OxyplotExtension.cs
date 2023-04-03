using OxyPlot;
using OxyPlot.Wpf;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class OxyplotExtension
{
    public static void Export(this PlotModel source, string path)
    {
        PngExporter.Export(source, path, 1920, 1080, 120);
    }

}
