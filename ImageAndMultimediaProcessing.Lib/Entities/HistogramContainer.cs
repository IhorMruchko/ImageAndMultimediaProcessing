using ImageAndMultimediaProcessing.Lib.Exceptions;
using ImageAndMultimediaProcessing.Lib.Extensions;
using ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;
using System.Collections.Generic;
using System.Drawing;

namespace ImageAndMultimediaProcessing.Lib.Entities;

public class HistogramContainer
{
    private readonly Dictionary<int, int> _redChanelAmount = new();
    private readonly Dictionary<int, int> _greenChanelAmount = new();
    private readonly Dictionary<int, int> _blueChanelAmount = new(); 

    public int Colors => 256;
    
    public int this[ImageChannels chanel, int value]
    {
        get
        {
            if (value < 0 || value > Colors)
                throw new RGBException(value);
            if (chanel == ImageChannels.Red && _redChanelAmount.TryGetValue(value, out var resultRed))
                return resultRed;
            if (chanel == ImageChannels.Green && _greenChanelAmount.TryGetValue(value, out var resultGreen))
                return resultGreen;
            if (chanel == ImageChannels.Blue && _blueChanelAmount.TryGetValue(value, out var blueValue))
                return blueValue;
            return 0;
        }
    }


    public HistogramContainer BuildHistogram(Bitmap image)
    {
        for (var i = 0; i < image.Width; ++i)
        {
            for (var j = 0; j < image.Height; ++j)
            {
                var currentPixel = image.GetPixel(i, j);
                _redChanelAmount.SetOrAdd(currentPixel.R);
                _greenChanelAmount.SetOrAdd(currentPixel.G);
                _blueChanelAmount.SetOrAdd(currentPixel.B);
            }
        }

        return this;
    }
}
