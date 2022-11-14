using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Adfcf.ExplorerCraft.Graphics.Imaging
{
    internal class Image2D
    {

        public int Width { get; }

        public int Height { get; }

        public byte[] Data { get; private set;  }

        public Image2D(byte[] data, int width, int height)
        {

            Data = new byte[width * height * 4];
            for (int i = 0; i < Data.Length; ++i)
            {
                Data[i] = (byte) data[i];
            }

            Width = width;
            Height = height;

        }

    }
}
