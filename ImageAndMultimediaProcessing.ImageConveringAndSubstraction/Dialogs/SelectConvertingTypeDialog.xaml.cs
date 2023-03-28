using ImageAndMultimediaProcessing.ImageConveringAndSubstraction.ImageTypeConverters;
using ImageAndMultimediaProcessing.Lib.Helpers.IO;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.Dialogs;

public partial class SelectConvertingTypeDialog : Window
{
    public SelectConvertingTypeDialog()
    {
        InitializeComponent();
        FilePathTextBlock.Text = Constants.IO.INITIAL_DIRECTORY;
        InitializeCombobox();
    }

    public string CurrentFormat => (string)FormatSelectorCombobox.SelectedValue;

    public string FilePath => Path.Combine(FilePathTextBlock.Text, FileNameTextBox.Text) + CurrentFormat;

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(FileNameTextBox.Text))
        {
            MessageBox.Show("Set file name");
            return;
        }
        DialogResult = true;
    }

    private void ChangeFilePath_Click(object sender, RoutedEventArgs e)
    {
        var saveDialog = IODialogHelper.CreateDirrectoryDialog(Constants.IO.INITIAL_DIRECTORY);
        if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            FilePathTextBlock.Text = saveDialog.SelectedPath;
        }
    }

    private void FileNameTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = Constants.IO.INVALID_CHARACTERS.Contains(e.Text[0]);
    }

    private void InitializeCombobox()
    {
        FormatSelectorCombobox.ItemsSource = ConvertersFactory.AcceptableFormats;
        FormatSelectorCombobox.SelectedIndex = 0;
    }

    private void FilePathTextBlock_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
        FilePathTextBlock.Focus();
        FilePathTextBlock.CaretIndex = FilePathTextBlock.Text.Length;
    }
}
