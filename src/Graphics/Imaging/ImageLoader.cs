using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Adfcf.ExplorerCraft.Graphics.Imaging
{
    internal static class ImageLoader
    {

        public static Image2D Load(string fileName)
        {

            var image = Image.Load<Rgba32>(fileName);
            image.Mutate(x => x.Flip(FlipMode.Vertical));

            byte[] pixels = new byte[image.Width * image.Height * 4];

            Span<Rgba32> row;
            for (int y = 0, index = 0; y < image.Height; ++y)
            {
                row = image.GetPixelRowSpan(y);
                for (int x = 0; x < image.Width; ++x)
                {
                    pixels[index++] = row[x].R;
                    pixels[index++] = row[x].G;
                    pixels[index++] = row[x].B;
                    pixels[index++] = row[x].A;
                }
            }

            return new Image2D(pixels, image.Width, image.Height);

        }

        public static Image2D[] Load(string fileName, int spritesPerColumn, int spritesPerRow)
        {

            Image2D[] images = new Image2D[spritesPerRow * spritesPerColumn];

            var image = Image.Load<Rgba32>(fileName);

            int spriteWidth = image.Width / spritesPerRow;
            int spriteHeight = image.Height / spritesPerColumn;

            Span<Rgba32> row;
            byte[] pixels = new byte[spriteWidth * spriteHeight * 4];
            for (int j = 0; j < spritesPerColumn; ++j)
            {
                for (int i = 0; i < spritesPerRow; ++i)
                {
                    for (int y = (spriteHeight * (j + 1)) - 1, index = 0; y >= spriteHeight * j; --y)
                    {
                        row = image.GetPixelRowSpan(y);
                        for (int x = (spriteWidth * (i + 1)) - 1; x >= i * spriteWidth; --x)
                        {
                            pixels[index++] = row[x].R;
                            pixels[index++] = row[x].G;
                            pixels[index++] = row[x].B;
                            pixels[index++] = row[x].A;
                        }
                    }
                    images[spritesPerRow * j + i] = new Image2D(pixels, spriteWidth, spriteHeight);
                }
            }

            return images;

        }

    }
}
