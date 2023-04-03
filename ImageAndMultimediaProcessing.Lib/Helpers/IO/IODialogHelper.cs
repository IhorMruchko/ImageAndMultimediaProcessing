using Microsoft.Win32;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace ImageAndMultimediaProcessing.Lib.Helpers.IO;

public static class IODialogHelper
{
    public static OpenFileDialog CreateOpenFileDialog(string initialDirectory, string fileFormatFilter) 
        => new()
        {
            Title = "Open file",
            Multiselect = true,
            InitialDirectory = initialDirectory,
            Filter = fileFormatFilter,
        };

    public static SaveFileDialog CreateSaveFileDialog(string initialDirectory, string fileFormatFilter)
        => new()
        {
            Title = "Save file to",
            InitialDirectory = initialDirectory,
            Filter = fileFormatFilter
        };

    public static FolderBrowserDialog CreateDirrectoryDialog(string initialDirectory)
    {
        return new()
        {
            InitialDirectory = initialDirectory,
        };
    }
}
