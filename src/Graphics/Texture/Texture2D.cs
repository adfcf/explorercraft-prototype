using Adfcf.ExplorerCraft.Graphics.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Adfcf.ExplorerCraft.Graphics.Texture
{
    internal class Texture2D : ITexture
    {

        int Id { get; set; }

        public Texture2D(string fileName)
        {

            var image2d = ImageLoader.Load(fileName);

            Id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, Id);

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Repeat);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image2d.Width, image2d.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image2d.Data);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            GL.BindTexture(TextureTarget.Texture2D, 0);

        }

        public void Use()
        {
            GL.BindTexture(TextureTarget.Texture2D, Id);
        }

        public void Delete()
        {
            GL.DeleteTexture(Id);
            Id = 0;
        }

    }
}
