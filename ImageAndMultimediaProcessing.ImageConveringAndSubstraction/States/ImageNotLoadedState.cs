using ImageAndMultimediaProcessing.Lib.Helpers.IO;
using System.Windows;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.States;

internal class ImageNotLoadedState : ImageWorkerState
{
    public ImageNotLoadedState(MainWindow context) : base(context)
    {
    }

    public override void Open()
    {
        var openFileDialog = IODialogHelper.CreateOpenFileDialog(Constants.IO.INITIAL_DIRECTORY,
                                                                  Constants.IO.BITMAP_FILE_FILTER);

        if (openFileDialog.ShowDialog() == true)
        {
            Context.ImageSource = openFileDialog.FileName;
            Context.ChangeState(new ImageLoadedState(Context));
        }
    }

    public override void Convert()
        => MessageBox.Show(Constants.Messages.IMAGE_NOT_LOADED,
                           Constants.Messages.Captions.IMAGE_NOT_LOADED,
                           MessageBoxButton.OK,
                           MessageBoxImage.Warning);

    public override void Substract() => Convert();
}
