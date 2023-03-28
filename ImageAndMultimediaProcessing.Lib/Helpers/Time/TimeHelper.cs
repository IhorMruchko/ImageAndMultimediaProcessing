using ImageAndMultimediaProcessing.Lib.Extensions;
using System;
using System.Diagnostics;

namespace ImageAndMultimediaProcessing.Lib.Helpers.Time;

public class TimeHelper
{
    private readonly Stopwatch _timer = new();

    public TimeResults Run(Action action)
    {
        _timer.Start();
        action.Invoke();
        
        _timer.Stop();
        var result = _timer.ToTimeResults();
        
        _timer.Reset();
        return result;
    }
}