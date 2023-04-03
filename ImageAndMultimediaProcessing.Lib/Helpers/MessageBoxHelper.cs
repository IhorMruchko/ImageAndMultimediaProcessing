using System.Windows;

namespace ImageAndMultimediaProcessing.Lib.Helpers;

public static class MessageBoxHelper
{
    public static void FileNotSelected() 
        => MessageBox.Show("Select image first", "Image not selected", MessageBoxButton.OK, MessageBoxImage.Warning);
}
