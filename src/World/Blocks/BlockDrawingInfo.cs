
namespace Adfcf.ExplorerCraft.World.Blocks
{
    internal struct BlockDrawingInfo
    {

        public readonly int id;

        public readonly float x;
        public readonly float y;
        public readonly float z;

        public BlockDrawingInfo(int id, float x, float y, float z)
        {

            this.id = id;

            this.x = x;
            this.y = y;
            this.z = z;

        }

    }
}
