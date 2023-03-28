using System.IO;

namespace ImageAndMultimediaProcessing.ImageConveringAndSubstraction;

public static class Constants
{
    public static class App
    {
        public const string TITLE = "Image worker";
    }

    public static class IO
    {
        public const string INITIAL_DIRECTORY = "E:\\Programming\\C#\\ImageAndMultimediaProcessing\\ImageAndMultimediaProcessing.ImageConveringAndSubstraction\\Resources\\Images";
        public const string BITMAP_FILE_FILTER = "Bitmaps|*.bmp";
        public const string IMAGE_FILE_FILTER = "Bitmaps|*.bmp|JPEG|*.jpeg|TIFF|*.tiff|Image files|*.bmp;*.jpg;|All files|*.*";

        public const string BMP = ".bmp";
        public const string BMP_PURE = ".bmp24";
        public const string JPEG = ".jpeg";
        public const string TIFF = ".tiff";

        public const int NONE = 0;
        public const int BMP_ORDER = 1;
        public const int JPEG_ORDER = 2;
        public const int TIFF_ORDER = 3;

        public static readonly char[] INVALID_CHARACTERS = Path.GetInvalidFileNameChars();
    }

    public static class Messages
    {
        public static class Captions
        {
            public const string IMAGE_NOT_LOADED = "File not found.";
        }
        public const string IMAGE_NOT_LOADED = "Image was not loaded. Open file first.";
    }

    public static class Formats
    {
        public const string TIME_ELAPSED = "{0} takes {1}";
    }
}
