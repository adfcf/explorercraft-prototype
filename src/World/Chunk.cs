using Adfcf.ExplorerCraft.Graphics;
using Adfcf.ExplorerCraft.World.Blocks;
using OpenTK.Mathematics;

namespace Adfcf.ExplorerCraft.World
{

    // 16x128x16
    internal class Chunk
    {

        public static int SizeX { get; } = 16;
        public static int SizeZ { get; } = 16;
        public static int SizeY { get; } = 128;

        readonly static byte Null = (byte) Block.Type.Air;

        public List<BlockDrawingInfo> DrawingInfo { get; private set; }

        readonly byte[,,] blocks;

        public float X { get; }
        public float Z { get; }

        public bool NeedsUpdate { get; private set; } = true;

        readonly static Random random = new Random();

        readonly Noise noise;
        readonly int seed;

        public Chunk(int seed, Noise noise, float cx, float cz)
        {

            this.noise = noise;
            this.seed = seed;

            X = cx;
            Z = cz;

            DrawingInfo = new();
            blocks = new byte[SizeX, SizeY, SizeZ];

            DefaultGeneration();

        }

        private void DefaultGeneration()
        {
            for (int x = 0; x < blocks.GetLength(0); ++x)
                for (int z = 0; z < blocks.GetLength(2); ++z)
                    GenerateHeight(x, z);
        }
        
        private void GenerateHeight(int x, int z)
        {
            int height = (int)(noise.Perlin(0.005 * (X + x), seed * 0.005f, 0.005 * (Z + z)) * SizeY);

            for (int y = height - 1; y < height; ++y)
            {
                if (y == 0)
                {
                    blocks[x, y, z] = (byte)Block.Type.Bedrock;
                }
                else if (y + 1 < height)
                {
                    blocks[x, y, z] = (byte)Block.Type.Dirt;
                }
                else if (y + 1 == height)
                {
                    blocks[x, y, z] = (byte)Block.Type.Grass;

                    if (1 == random.Next(200))
                    {
                        GenerateTree(x, y, z);
                    }

                }
            }

        }

        public void GenerateTree(int x, int y, int z)
        {

            if (x == 0 || y == 0 || z == 0 || x + 1 >= SizeX || y + 12 >= SizeY || z + 1 >= SizeZ)
                return;

            if (blocks[x + 1, y, z + 1] != Null || 
                blocks[x + 1, y, z - 1] != Null || 
                blocks[x + 1, y, z + 0] != Null ||
                blocks[x - 1, y, z + 1] != Null || 
                blocks[x - 1, y, z - 1] != Null || 
                blocks[x - 1, y, z + 0] != Null ||
                blocks[x + 0, y, z + 1] != Null || 
                blocks[x + 0, y, z - 1] != Null || 
                blocks[x + 0, y, z + 0] != Null)
            {
                // return;
            }

            int treeHeight = random.Next(3, 10);
            for (int i = 0; i < treeHeight; ++i)
            {
                blocks[x, y + i, z] = (byte)Block.Type.Wood;
            }

            blocks[x + 1, y + treeHeight - 1, z] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight - 1, z] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight - 1, z + 1] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight - 1, z - 1] = (byte)Block.Type.Leaves;

            blocks[x, y + treeHeight, z] = (byte)Block.Type.Leaves;
            blocks[x + 1, y + treeHeight, z + 1] = (byte)Block.Type.Leaves;
            blocks[x + 1, y + treeHeight, z - 1] = (byte)Block.Type.Leaves;
            blocks[x + 1, y + treeHeight, z] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight, z + 1] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight, z - 1] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight, z] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight, z + 1] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight, z - 1] = (byte)Block.Type.Leaves;

            blocks[x, y + treeHeight + 1, z] = (byte)Block.Type.Leaves;
            blocks[x + 1, y + treeHeight + 1, z + 1] = (byte)Block.Type.Leaves;
            blocks[x + 1, y + treeHeight + 1, z - 1] = (byte)Block.Type.Leaves;
            blocks[x + 1, y + treeHeight + 1, z] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight + 1, z + 1] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight + 1, z - 1] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight + 1, z] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight + 1, z + 1] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight + 1, z - 1] = (byte)Block.Type.Leaves;

            blocks[x + 1, y + treeHeight + 2, z] = (byte)Block.Type.Leaves;
            blocks[x - 1, y + treeHeight + 2, z] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight + 2, z + 1] = (byte)Block.Type.Leaves;
            blocks[x, y + treeHeight + 2, z - 1] = (byte)Block.Type.Leaves;

        }

        // this costs a lot!!!
        public void Update()
        {

            if (!NeedsUpdate)
                return;

            DrawingInfo.Clear();

            byte id;
            for (int x = 0; x < blocks.GetLength(0); ++x)
            {
                for (int y = 0; y < blocks.GetLength(1); ++y)
                {
                    for (int z = 0; z < blocks.GetLength(2); ++z)
                    {

                        id = blocks[x, y, z];

                        if (id == Null)
                            continue;



                        DrawingInfo.Add(new BlockDrawingInfo(id, X + x, y, Z + z));

                    }
                }
            }

            NeedsUpdate = false;

        }

        public void DestroyBlock(int x, int y, int z)
        {
            blocks[x, y, z] = Null;
            NeedsUpdate = true;
        }

        public Vector3i? CloserBlockIntersection(Vector3 start, Vector3 direction, int reach)
        {
            Vector3i? intersection;
            for (float d = 0; d < reach; d += 0.5f)
            {
                intersection = BlockIntersection(start);
                if (intersection.HasValue)
                    return intersection.Value;
                start += direction * d;
            }
            return null;
        }

        public Vector3i? BlockIntersection(Vector3 point)
        {
            int bi = SizeX * (int)Math.Round((X + SizeX) / point.X);
            int bj = SizeY * (int)Math.Round(SizeY / point.Y); ;
            int bk = SizeZ * (int)Math.Round((Z + SizeZ) / point.Z); ;
            return new Vector3i(bi, bj, bk);
        }

        public float CenterX()
        {
            return X + (SizeX / 2);
        }

        public float CenterZ()
        {
            return Z + (SizeZ / 2);
        }

        public float CenterY()
        {
            return (SizeY / 2);
        }

    }
}
