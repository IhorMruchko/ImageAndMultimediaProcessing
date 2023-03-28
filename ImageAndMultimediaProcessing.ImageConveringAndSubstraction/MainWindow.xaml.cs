using ImageAndMultimediaProcessing.ImageConveringAndSubstraction.States;
using ImageAndMultimediaProcessing.Lib.Extensions;
using ImageAndMultimediaProcessing.Lib.Helpers.Time;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction;

public partial class MainWindow : Window
{
    private readonly Logger _logger = new();

    private string _imageSource = string.Empty;
    private ImageWorkerState _currentState;

    public MainWindow()
    {
        InitializeComponent();
        Title = Constants.App.TITLE;
        _currentState = new ImageNotLoadedState(this);
    }

    public string ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            if (value == string.Empty)
            {
                ImageContainer.Source = null;
                return;
            }
            ImageContainer.Source = new BitmapImage().LoadImage(value);
        }
    }
    private void Open_Click(object sender, RoutedEventArgs e)
    {
        _currentState.Open();
    }

    private void Convert_Click(object sender, RoutedEventArgs e)
    {
        _currentState.Convert();
    }

    internal void ChangeState(ImageWorkerState imageLoadedState)
    {
        _currentState = imageLoadedState;
    }

    internal void Log(string methodName, string message)
    {
        var logInfo = _logger.Log(methodName, message);
        LogResultsTextBox.Text += logInfo;
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        _logger.Save();
    }

    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        ImageSource = string.Empty;
        ChangeState(new ImageNotLoadedState(this));
    }

    private void Subtract_Click(object sender, RoutedEventArgs e)
    {
        _currentState.Substract();
    }
}
