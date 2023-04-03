using ImageAndMultimediaProcessing.Lib.Entities.Enums;
using ImageAndMultimediaProcessing.Lib.Extensions;
using ImageAndMultimediaProcessing.Lib.Helpers;
using ImageAndMultimediaProcessing.Lib.Helpers.IO;
using ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageAndMultimediaProcessing.EqulizeAndFilters;

public partial class MainWindow : Window
{
    private List<string> _filePathes = new();

    private Dictionary<(bool, bool, bool, bool), Action<string>> _operationTransition;

    public MainWindow()
    {
        InitializeComponent();
        _operationTransition = new Dictionary<(bool, bool, bool, bool), Action<string>>()
        {
            [(true, false, false, false)] = Equaalize,
            [(false, true, false, false)] = Roberts,
            [(false, false, true, false)] = Prewitt,
            [(false, false, false, true)] = Sobel,
        };
    }

   
    public string? CurrentFilePath 
        => FilePathSelectorCombobox.SelectedIndex < 0 
        ? null 
        : _filePathes[FilePathSelectorCombobox.SelectedIndex];

    public string? CurrentFileName => FilePathSelectorCombobox.SelectedItem as string;

    public bool IsFileSelected => CurrentFilePath is not null;

    private void AddFileButton_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = IODialogHelper.CreateOpenFileDialog(AppConstants.CurrentDirectory, AppConstants.BMP_FILTER);
        if (openFileDialog.ShowDialog() == false)
        {
            return;
        }
        UpdateFiles(openFileDialog.FileNames);
    }

    private void FilePathSelectorCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        CurrentImagePlaceholder.Source = new BitmapImage().LoadImage(_filePathes[FilePathSelectorCombobox.SelectedIndex]);
        CurrentImageTitleTextBlock.Text = FilePathSelectorCombobox.SelectedItem as string;
        ModifiedImagePlaceholder.Source = null;
        ClearHistograms();
    }

    private void ClearHistograms()
    {
        RedChanelHistogram.Model?.Series.Clear();
        RedChanelHistogram.InvalidatePlot(true);
        GreenChanelHistogram.Model?.Series.Clear();
        GreenChanelHistogram.InvalidatePlot(true);
        BlueChanelHistogram.Model?.Series.Clear();
        BlueChanelHistogram.InvalidatePlot(true);
    }

    private void UpdateFiles(string[] fileNames)
    {
        _filePathes.AddRange(fileNames);
        _filePathes = _filePathes.Distinct().ToList();
        FilePathSelectorCombobox.ItemsSource = _filePathes.Select(filePath => Path.GetFileNameWithoutExtension(filePath)).ToList();
    }

    private void BuildHistogramsButton_Click(object sender, RoutedEventArgs e)
    {
        if (IsFileSelected == false)
        {
            MessageBoxHelper.FileNotSelected();
            return;
        }

        var currentImageHistogram = new Bitmap(CurrentFilePath!).CreateHistogram();
        var redModel = new PlotModel() { Title = $"Red channel histogram ({CurrentFileName})" };
        var greenModel = new PlotModel() { Title = $"Green channel histogram ({CurrentFileName})" };
        var blueModel = new PlotModel() { Title = $"Blue channel histogram ({CurrentFileName})" };
        var redSeries = new HistogramSeries() { FillColor = OxyColors.Red };
        var greenSeries = new HistogramSeries() { FillColor = OxyColors.Green };
        var blueSeries = new HistogramSeries() { FillColor = OxyColors.Blue };
        for (var i = 0; i < currentImageHistogram.Colors; ++i)
        {
            redSeries.Items.Add(new HistogramItem(i-0.5, i+0.5, currentImageHistogram[ImageChannels.Red, i], i));
            greenSeries.Items.Add(new HistogramItem(i-0.5, i+0.5, currentImageHistogram[ImageChannels.Green, i], i));
            blueSeries.Items.Add(new HistogramItem(i-0.5, i + 0.5, currentImageHistogram[ImageChannels.Blue, i], i));
        }

        redModel.Series.Add(redSeries);
        greenModel.Series.Add(greenSeries);
        blueModel.Series.Add(blueSeries);

        redModel.Export(Path.Combine(AppConstants.HistogramDirectory, $"{CurrentFileName}_red.bmp"));
        greenModel.Export(Path.Combine(AppConstants.HistogramDirectory, $"{CurrentFileName}_green.bmp"));
        blueModel.Export(Path.Combine(AppConstants.HistogramDirectory, $"{CurrentFileName}_blue.bmp"));

        RedChanelHistogram.Model = redModel;
        GreenChanelHistogram.Model = greenModel;
        BlueChanelHistogram.Model = blueModel;
        HistogramTab.IsSelected = true;
    }

    private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
    {
        if (IsFileSelected == false)
        {
            MessageBoxHelper.FileNotSelected();
            return;
        }
        var saveFileDialog = IODialogHelper.CreateSaveFileDialog(AppConstants.CurrentDirectory, AppConstants.BMP_FILTER);
        var key = (EqualizeOperationRadioButton.IsChecked ?? false,
                RobertsFiltereOperationRadioButton.IsChecked ?? false,
                PrewittFiltereOperationRadioButton.IsChecked ?? false,
                SobelFiltereOperationRadioButton.IsChecked ?? false);
        if (_operationTransition.ContainsKey(key) && saveFileDialog.ShowDialog() == true)
        {
            _operationTransition[key](saveFileDialog.FileName);
        }
    }

    private void Equaalize(string filePath)
    {
        new Bitmap(CurrentFilePath!).Equalize().Save(filePath);
        ModifiedImagePlaceholder.Source = new BitmapImage().LoadImage(filePath);
    }

    private void Roberts(string filePath)
    {
        new Bitmap(CurrentFilePath!).ApplyFilter(Filters.Roberts).Save(filePath);
        ModifiedImagePlaceholder.Source = new BitmapImage().LoadImage(filePath);
    }

    private void Prewitt(string filePath)
    {
        new Bitmap(CurrentFilePath!).ApplyFilter(Filters.Prewitt).Save(filePath);
        ModifiedImagePlaceholder.Source = new BitmapImage().LoadImage(filePath);
    }

    private void Sobel(string filePath)
    {
        new Bitmap(CurrentFilePath!).ApplyFilter(Filters.Sobel).Save(filePath);
        ModifiedImagePlaceholder.Source = new BitmapImage().LoadImage(filePath);
    }

}
