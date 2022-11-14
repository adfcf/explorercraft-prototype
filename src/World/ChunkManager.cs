
using Adfcf.ExplorerCraft;
using Adfcf.ExplorerCraft.Graphics;
using Adfcf.ExplorerCraft.Mathematics;
using Adfcf.ExplorerCraft.World.Blocks;

namespace Adfcf.ExplorerCraft.World
{
    internal class ChunkManager
    {

        List<Chunk> loadedChunks;

        Player player;

        public ChunkManager(Player player)
        {
            loadedChunks = new();
            this.player = player;
        }

        public void AddChunk(Chunk chunk)
        {
            loadedChunks.Add(chunk);
        }

        public void Clear()
        {
            loadedChunks.Clear();   
        }

        public void Update(double deltaTime)
        {
            loadedChunks.FindAll(chunk => chunk.NeedsUpdate).ForEach(chunk => chunk.Update());
        }

        public void DestroyBlock()
        {
            Console.WriteLine("Destroy block...");
        }

        public void Render(Renderer renderer, double deltaTime)
        {
            int count = 0;
            renderer.Begin();
            foreach (var chunk in loadedChunks)
            {
                if (player.CanSee(chunk))
                {
                    ++count;
                    foreach (var block in chunk.DrawingInfo)
                    {
                        renderer.DrawBlock(block);
                    }
                }
            }
            renderer.End();
            //Console.WriteLine("{0}/{1} chunks", count, loadedChunks.Count);
        }

    }
}
