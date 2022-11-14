using Adfcf.ExplorerCraft.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adfcf.ExplorerCraft.World.Blocks
{
    internal class Block
    {

        public enum Type
        {
            Air,
            Bedrock,
            Stone,
            Cobblestone,
            Gravel,
            Sand,
            Dirt,
            Grass,
            Snow,
            Water,
            Ice,
            Clay,
            ClayBricks,
            StoneBricks,
            Granite,
            Wood,
            Leaves,
            WoodPlanks
        }

        private static readonly Block[] blocks = {
            new("Air", 0, false),
            new("Bedrock", 0, true),
            new("Stone", 1, true),
            new("Cobblestone", 2, true),
            new("Gravel", 3, true),
            new("Sand", 4, true),
            new("Dirt", 5, true),
            new("Grass", new Vec4(7, 7, 6, 5), true),
            new("Snow", new Vec4(9, 9, 8, 5), true),
            new("Water", 10, false),
            new("Ice", 11, true),
            new("Clay", 12, true),
            new("Clay Bricks", 13, true),
            new("Stone Bricks", 14, true),
            new("Granite", 15, true),
            new("Wood", new Vec4(17, 17, 16, 16), true),
            new("Leaves", 18, true),
            new("Wood Planks", 19, true),
        };

        public int Id { get; }
        public string Name { get; }
        public bool Solid { get; }

        public Vec4 Texture { get; }

        public static int Total { get; private set; }


        private Block(string name, Vec4 texture, bool solid)
        {

            Id = Total++;

            Name = name;
            Solid = solid;
            Texture = texture;

        }

        private Block(string name, int texture, bool solid) : 
            this(name, new Vec4(texture, texture, texture, texture), solid)
        {}

        public static Block GetById(int id) => blocks[id];

    }
}
