using ImageAndMultimediaProcessing.Lib.Entities;
using ImageAndMultimediaProcessing.Lib.Exceptions;
using System;
using System.Drawing;
using static System.Math;

namespace ImageAndMultimediaProcessing.Lib.Extensions;

public static class MatrixExtension
{
    public static int Scalar<TElements>(this Matrix<int> source, Matrix<TElements> second, Func<TElements, int> selector)
    {
        if (source.SameSizes(second) == false)
        {
            throw new MatrixDimensionException<int, TElements>(source, second);
        }
        var sum = 0;
        for (var i = 0; i < source.Height; ++i)
        {
            for (var j = 0; j < source.Width; ++j)
            {
                sum += source[i, j] * selector(second[i, j]);
            }
        }
        return sum;
    }

    public static double ScalarDistance<TElements>(this Matrix<TElements> source, Matrix<int> horizontalOperator,
        Matrix<int> verticalOperator, Func<TElements, int> selector)
    {
        return Sqrt(Pow(horizontalOperator.Scalar(source, selector), 2) + Pow(verticalOperator.Scalar(source, selector), 2));
    }

    public static double ScalarDistance(this Matrix<Color> source, Matrix<int> horizontalOperator,
        Matrix<int> verticalOperator) 
        => source.ScalarDistance(horizontalOperator, verticalOperator, color => color.R)
            + source.ScalarDistance(horizontalOperator, verticalOperator, color => color.G)
            + source.ScalarDistance(horizontalOperator, verticalOperator, color => color.B);
}
