using ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;
using ImageAndMultimediaProcessing.Lib.Helpers.Time;
using ImageAndMultimediaProcessing.Lib.Extensions;
using System;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.States;

internal abstract class ImageProcessingState : ImageWorkerState
{
    protected TimeHelper timer = new();
    protected byte[] convertedImage = Array.Empty<byte>();

    protected ImageProcessingState(MainWindow context) : base(context)
    {
    }

    protected void Convert(IImageConverter converter)
    {
        var timeResults = timer.Run(() => convertedImage = converter.Convert(convertedImage));
        Context!.Log(nameof(converter.Convert),
                            Constants.Formats.TIME_ELAPSED.Format(nameof(converter.Convert),
                                                                  timeResults.Elapsed));
    }

    protected void Write(IImageConverter converter, string filePath)
    {
        var timeResults = timer.Run(() => converter.Write(convertedImage, filePath));
        Context!.Log(nameof(converter.Write),
                            Constants.Formats.TIME_ELAPSED.Format(nameof(converter.Write),
                                                                  timeResults.Elapsed));
    }

    protected void Read(IImageConverter converter, string filePath)
    {
        var timeResults = timer.Run(() => convertedImage = converter.Read(filePath));
        Context!.Log(nameof(converter.Read),
                            Constants.Formats.TIME_ELAPSED.Format(nameof(converter.Read),
                                                                  timeResults.Elapsed));
    }

    protected void Decode(IImageConverter converter)
    {
        var timeResults = timer.Run(() => converter.Decode(convertedImage));
        Context!.Log(nameof(converter.Decode),
                            Constants.Formats.TIME_ELAPSED.Format(nameof(converter.Decode),
                                                                  timeResults.Elapsed));
    }
}
