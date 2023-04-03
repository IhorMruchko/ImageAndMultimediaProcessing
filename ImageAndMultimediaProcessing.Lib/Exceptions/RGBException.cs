using System;

namespace ImageAndMultimediaProcessing.Lib.Exceptions;

public class RGBException : Exception
{
    public RGBException(int value)
        : base($"{value} must be between 0 and 256") { }
}
