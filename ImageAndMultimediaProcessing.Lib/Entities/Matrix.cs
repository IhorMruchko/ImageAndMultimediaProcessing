using System;
using System.Linq;
using ImageAndMultimediaProcessing.Lib.Extensions;

namespace ImageAndMultimediaProcessing.Lib.Entities;

public class Matrix<TElements>
{
    private readonly int _width;
    private readonly int _height;

    TElements[][] _elements;

    public int Width => _width;
    public int Height => _height;

    public static Matrix<TElements> Empty => new(0, 0);

    public TElements this[Index row, Index column]
    {
        get => _elements[row][column];
        set => _elements[row][column] = value;
    }

    public Matrix(int height, int width)
    {
        _width = width;
        _height = height;
        _elements = new TElements[height][];
        for (var i = 0; i < height; i++)
        {
            _elements[i] = new TElements[width];
        }
    }

    public bool SameSizes<TSecond>(Matrix<TSecond> second)
        => Height == second.Height && Width == second.Width;

    public override string ToString() 
        => "\n".JoinStrings(_elements.Select(row => "\t".JoinStrings(row)));

    public static implicit operator Matrix<TElements>(TElements[][] source)
        => source.Length == 0
        ? Matrix<TElements>.Empty
        : new(source.Length, source[0].Length)
        {
            _elements = (TElements[][])source.Clone()
        };

    public static implicit operator Matrix<TElements>(TElements[,] source)
       => new(source.GetLength(0), source.GetLength(1))
       {
           _elements = Enumerable.Range(0, source.GetLength(0))
                                 .Select(i => Enumerable.Range(0, source.GetLength(1))
                                                        .Select(j => source[i, j])
                                                        .ToArray())
                                 .ToArray()
       };

    public static Matrix<TElements> operator !(Matrix<TElements> source)
        => new(source.Width, source.Height)
        {
            _elements = Enumerable.Range(0, source.Width)
                                  .Select(j => Enumerable.Range(0, source.Height)
                                                         .Select(i => source[i, j])
                                                         .ToArray())
                                  .ToArray()
        };
}
