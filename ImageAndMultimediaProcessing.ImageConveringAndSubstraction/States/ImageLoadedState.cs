using ImageAndMultimediaProcessing.ImageConveringAndSubstraction.Dialogs;
using ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;
using ImageAndMultimediaProcessing.Lib.Extensions;
using ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;
using System;
using System.IO;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.States;

internal class ImageLoadedState : ImageProcessingState
{
    public ImageLoadedState(MainWindow context) : base(context)
    {
    }

    public override void Open()
    {
        var converter = ConvertersFactory.GetConverter<BmpImageConverter>();
        if (converter is null) return;
        Read(converter, Context.ImageSource);
        Write(converter, Context.ImageSource);
        Decode(converter);
        Context.ChangeState(new ImageLoadedState(Context));
    }

    public override void Convert()
    {
        var selectFileFormatDialog = new SelectConvertingTypeDialog();
        if (selectFileFormatDialog.ShowDialog() == false) return;

        var converter = ConvertersFactory.GetConverter(selectFileFormatDialog.CurrentFormat);
        if (converter is null || Context is null) return;

        convertedImage = File.ReadAllBytes(Context.ImageSource);
        Convert(converter);
        Write(converter, selectFileFormatDialog.FilePath);
        Read(converter, selectFileFormatDialog.FilePath);
        Decode(converter);
    }

    public override void Substract()
    {
        var substractionDialog = new SelectSubstractingDialog();
        if (substractionDialog.ShowDialog() == false || Context is null) return;

        var (R, G, B, Image, chanels) = Context.ImageSource.ToBitmap()!.Substract(substractionDialog.SubstractFilePath.ToBitmap()!);
        R.Save(substractionDialog.DirectoryPath.FormPath(Context.ImageSource,
                                                         ImageChannels.Red,
                                                         Path.GetExtension(substractionDialog.SubstractFilePath)));
        G.Save(substractionDialog.DirectoryPath.FormPath(Context.ImageSource,
                                                         ImageChannels.Green,
                                                         Path.GetExtension(substractionDialog.SubstractFilePath)));
        B.Save(substractionDialog.DirectoryPath.FormPath(Context.ImageSource,
                                                         ImageChannels.Blue,
                                                         Path.GetExtension(substractionDialog.SubstractFilePath)));
        Image.Save(substractionDialog.DirectoryPath.FormPath(Context.ImageSource,
                                                             ImageChannels.All,
                                                             Path.GetExtension(substractionDialog.SubstractFilePath)));
        Context.Log(nameof(Substract), Environment.NewLine + chanels.GetStatistics());
    }
}
