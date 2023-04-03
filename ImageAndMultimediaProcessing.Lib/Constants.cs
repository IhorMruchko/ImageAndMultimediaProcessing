using ImageAndMultimediaProcessing.Lib.Entities;

namespace ImageAndMultimediaProcessing.Lib;

public static class Constants
{
    public static readonly Matrix<int> HorizontalRobertsOperator = new int[,]
    {
        { 0, -1 },
        { 1, 0 }
    };

    public static readonly Matrix<int> VerticalRobertsOperator = new int[,]
    {
        {-1, 0 },
        { 0, 1 }
    };

    public static readonly Matrix<int> HorizontalPrewittOperator = new int[,]
    {
         { 1, 0, -1 },
         { 1, 0, -1 },
         { 1, 0, -1 }
    };

    public static readonly Matrix<int> VerticalPrewittOperator = new int[,]
    {
         {-1,-1,-1 },
         { 0, 0, 0 },
         { 1, 1, 1 }
    };

    public static readonly Matrix<int> HorizontalSobelOperator = new int[,]
    {
         { 1, 0, -1 },
         { 2, 0, -2 },
         { 1, 0, -1 }
    };

    public static readonly Matrix<int> VerticalSobelOperator = new int[,]
    {
        {-1,-2,-1 },
        { 0, 0, 0 },
        { 1, 2, 1 }
    };
}
