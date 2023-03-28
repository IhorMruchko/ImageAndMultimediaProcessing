using ImageAndMultimediaProcessing.Lib.Helpers.IO;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DR = System.Windows.Forms.DialogResult;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.Dialogs;

public partial class SelectSubstractingDialog : Window
{
    public SelectSubstractingDialog()
    {
        InitializeComponent();
        SelectedDirectoryTextBox.Text = Constants.IO.INITIAL_DIRECTORY;
        SubtractFilePathTextBox.Text = Constants.IO.INITIAL_DIRECTORY;
    }

    public string DirectoryPath => SelectedDirectoryTextBox.Text;

    public string SubstractFilePath => SubtractFilePathTextBox.Text;

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox) return;
        textBox.Focus();
        textBox.CaretIndex = textBox.Text.Length;
    }

    private void SubtractionFileSelector_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = IODialogHelper.CreateOpenFileDialog(Constants.IO.INITIAL_DIRECTORY,
                                                                 Constants.IO.IMAGE_FILE_FILTER);

        if (openFileDialog.ShowDialog() == true)
        {
            SubtractFilePathTextBox.Text = openFileDialog.FileName;
        }
    }
    private void DirectoryFileSelector_Click(object sender, RoutedEventArgs e)
    {
        var directoryDialog = IODialogHelper.CreateDirrectoryDialog(Constants.IO.INITIAL_DIRECTORY);

        if (directoryDialog.ShowDialog() == DR.OK)
        {
            SelectedDirectoryTextBox.Text = directoryDialog.SelectedPath;
        }
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
        if (File.Exists(SubstractFilePath) == false || Directory.Exists(DirectoryPath) == false)
        {
            MessageBox.Show("Input proper values");
            return;
        }
        DialogResult = true;
    }

}
