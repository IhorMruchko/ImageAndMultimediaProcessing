using ImageAndMultimediaProcessing.Lib.Helpers.Time;
using System.Diagnostics;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class StopWatchExtension
{
    public static TimeResults ToTimeResults(this Stopwatch timer) 
        => new ()
        {
            Elapsed = timer.Elapsed,
            ElapsedTicks = timer.ElapsedTicks,
            ElapsedMiliseconds = timer.ElapsedMilliseconds
        };
}
