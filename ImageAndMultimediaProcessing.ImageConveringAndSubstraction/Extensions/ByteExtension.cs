using ImageMagick;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction.Extensions;

public static class ByteExtension
{
    public static byte[] Quantize(this byte[] source, QuantizeSettings settings)
    {
        var quantized = new MagickImage(source)
        {
            ColorType = ColorType.Palette,
            Depth = 8
        };
        quantized.Quantize(settings);
        return quantized.ToByteArray();  
    }
}
