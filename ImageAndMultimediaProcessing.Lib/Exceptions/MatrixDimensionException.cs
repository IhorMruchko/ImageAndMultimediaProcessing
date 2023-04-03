using ImageAndMultimediaProcessing.Lib.Entities;
using ImageAndMultimediaProcessing.Lib.Extensions;
using System;

namespace ImageAndMultimediaProcessing.Lib.Exceptions;

public class MatrixDimensionException<TFirst, TSecond> : Exception
{
    private const string EXCEPTION_MESSAGE = "Matrix has no equal sizes: Height {0} - {1}; Width {2} - {3}";
    
    public MatrixDimensionException(Matrix<TFirst> first, Matrix<TSecond> second)
        : base(EXCEPTION_MESSAGE.Format(first.Height, second.Height, first.Width, second.Width))
    {
    }
}
