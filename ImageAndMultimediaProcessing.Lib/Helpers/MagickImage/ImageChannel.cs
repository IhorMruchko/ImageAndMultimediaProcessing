namespace ImageAndMultimediaProcessing.Lib.Helpers.MagickImage;

public class ImageChannel
{
    private readonly short[,] _colorMatrix;
    private short _minColor = 255;
    private short _maxColor;

    public ImageChannel(int width, int height)
    {
        _colorMatrix = new short[width, height];
    }

    public int PlusPixelsAmount { get; private set; }

    public int ZeroPixelsAmount { get; private set; }

    public int MinusPixelsAmount { get; private set; }

    public void ComparePixels(byte first, byte second, int xIndex, int yIndex)
    {
        var differ = IncreaseAmount(first - second);
        _colorMatrix[xIndex, yIndex] = differ;
        UpdateBorderValues(differ);
    }

    public int GetNormalizedPixel(int xIndex, int yIndex)
    {
        return _maxColor - _minColor == 0
            ? 0
            : (int)((_colorMatrix[xIndex, yIndex] - _minColor) / (float)(_maxColor - _minColor) * 255);
    }

    private void UpdateBorderValues(short differ)
    {
        if (_minColor > differ)
        {
            _minColor = differ;
        }
        if (_maxColor < differ)
        {
            _maxColor = differ;
        }
    }

    private short IncreaseAmount(int differance)
    {
        if (differance < 0)
        {
            ++MinusPixelsAmount;
        }
        if (differance > 0)
        {
            ++PlusPixelsAmount;
        }
        if (differance == 0)
        {
            ++ZeroPixelsAmount;
        }
        return (short)differance;
    }
}
