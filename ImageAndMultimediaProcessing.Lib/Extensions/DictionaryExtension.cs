using System.Collections.Generic;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class DictionaryExtension
{
    public static void SetOrAdd(this Dictionary<int, int> source, int value)
    {
        if (source.ContainsKey(value))
        {
            source[value] += 1;
        }
        else
        {
            source[value] = 1;
        }
    }
}
