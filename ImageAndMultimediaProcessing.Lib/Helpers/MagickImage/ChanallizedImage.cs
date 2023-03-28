using ImageAndMultimediaProcessing.Lib.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;

public class ChanallizedImage
{
    private const string STATISTIC_FORMAT = "{0, 5}: {1, 5}|{2, 5}|{3,5}\n";
    private readonly Dictionary<ImageChannels, Func<int, int, Color>> _chanelToColorTransition;

    private readonly ImageChannel _redChanel;
    private readonly ImageChannel _greenChanel;
    private readonly ImageChannel _blueChanel;

    public ChanallizedImage(int widht, int height)
    {
        _redChanel = new ImageChannel(widht, height);
        _greenChanel = new ImageChannel(widht, height);
        _blueChanel = new ImageChannel(widht, height);
        _chanelToColorTransition = new()
        {
            [ImageChannels.Red] = (x, y) => Color.FromArgb(_redChanel.GetNormalizedPixel(x, y), 0, 0),
            [ImageChannels.Green] = (x, y) => Color.FromArgb(0, _greenChanel.GetNormalizedPixel(x, y), 0),
            [ImageChannels.Blue] = (x, y) => Color.FromArgb(0, 0, _blueChanel.GetNormalizedPixel(x, y)),
            [ImageChannels.All] = (x, y) => Color.FromArgb(_redChanel.GetNormalizedPixel(x, y),
                                                           _greenChanel.GetNormalizedPixel(x, y),
                                                           _blueChanel.GetNormalizedPixel(x, y))
        };
    }

    public string GetStatistics()
        => STATISTIC_FORMAT.Format("Red",
                                   _redChanel.MinusPixelsAmount,
                                   _redChanel.ZeroPixelsAmount,
                                   _redChanel.PlusPixelsAmount)
        + STATISTIC_FORMAT.Format("Green",
                                  _greenChanel.MinusPixelsAmount,
                                  _greenChanel.ZeroPixelsAmount,
                                  _redChanel.PlusPixelsAmount)
        + STATISTIC_FORMAT.Format("Blue",
                                  _blueChanel.MinusPixelsAmount,
                                  _blueChanel.ZeroPixelsAmount,
                                  _blueChanel.PlusPixelsAmount);

    public void SubstractColors(Color firstColor, Color secondColor, int xIndex, int yIndex)
    {
        _redChanel.ComparePixels(firstColor.R, secondColor.R, xIndex, yIndex);
        _greenChanel.ComparePixels(firstColor.G, secondColor.G, xIndex, yIndex);
        _blueChanel.ComparePixels(firstColor.B, secondColor.B, xIndex, yIndex);
    }

    internal Color GetColor(ImageChannels channel, int xIndex, int yIndex)
        => _chanelToColorTransition[channel].Invoke(xIndex, yIndex);
}
