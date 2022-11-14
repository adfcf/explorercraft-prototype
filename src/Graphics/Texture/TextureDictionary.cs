using System.Collections.Generic;

namespace Adfcf.ExplorerCraft.Graphics.Texture
{
    internal class TextureDictionary
    {

        Dictionary<Type, ITexture> Dictionary { get; } = new();

        public void Init()
        {
            Dictionary.Add(Type.Blocks, new Texture2DArray("Resources/Blocks.png", 8, 4));
        }

        public void UnloadAll()
        {
            foreach (var texture in Dictionary.Values)
            {
                texture.Delete();
            }
            Dictionary.Clear();
        }

        public void Use(Type texture)
        {
            Dictionary[texture].Use();
        }

        public enum Type
        {
            Default,
            Blocks
        }

    }
}
