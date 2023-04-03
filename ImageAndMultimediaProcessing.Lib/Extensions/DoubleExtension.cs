using System;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class DoubleExtension
{
    public static int Normalize(this double value, double min, double max, double coefficient)
    {
        return (int)Math.Round(coefficient * (value - min) / (max - min));
    }
}
