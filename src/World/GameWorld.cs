using Adfcf.ExplorerCraft;
using Adfcf.ExplorerCraft.Graphics;
using Adfcf.ExplorerCraft.Graphics.Texture;
using Adfcf.ExplorerCraft.Mathematics;
using Adfcf.ExplorerCraft.World.Blocks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Adfcf.ExplorerCraft.World
{
    internal class GameWorld
    {

        public ChunkManager ChunkManager { get; private set; }
        public Player Player { get; private set; }
        public DayCycle Time { get; }

        ExplorerCraft Game { get; set; }

        Noise noise;

        public GameWorld(ExplorerCraft game)
        {

            Time = new();
            Time.Speed = 60 * 30;

            Game = game;
            Player = new(game, this, new(0.0f, 70.0f, 0.0f));
            ChunkManager = new(Player);
            noise = new();

        }

        public void Load()
        {
            Player.Load();

            int xs = 75;
            int zs = 75;

            int seed = new Random().Next();
            for (int x = 0; x < xs; ++x)
            {
                for (int z = 0; z < zs; ++z)
                {
                    ChunkManager.AddChunk(new Chunk(seed, noise, x * Chunk.SizeX, z * Chunk.SizeZ));
                    //Console.WriteLine("Generating world... {0}/{1}", count++, xs * zs);
                }
            }

        }

        public void Update(double deltaTime)
        {

            if (Game.IsKeyDown(Keys.K))
                Time.Tick();

            Player.Update(deltaTime);
            ChunkManager.Update(deltaTime);

            Game.Title = Time.ToString() + ", " + Player.FirstCamera.GetPosition();

        }

        public void Render(Renderer renderer, double deltaTime)
        {

            renderer.SetBackgroundColor(GetBackgroundColor());
            renderer.SetAmbient(GetAmbientLight(), GetUniversalLightRay());

            renderer.UpdateCamera(Player.FirstCamera);
            Player.Draw(renderer, deltaTime);
            ChunkManager.Render(renderer, deltaTime);

        }

        private Vec3 GetAmbientLight()
        {

            float darkness = Math.Abs(Time.Current - 0.5f) * 2;
            float component = MathF.Max(1.0f - darkness, 0.1f) / 2.0f;

            return new(component, component, component);

        }

        private Vec3 GetUniversalLightRay()
        {
            Vec3 sunlightRay = new();

            float sunlight = MathF.Abs(Time.Current <= 0.5 ? Time.Current : Time.Current - 1.0f) * 2.0f;
            float angle = sunlight * (MathF.PI / 2.0f);

            sunlightRay.x = MathF.Cos(angle);
            sunlightRay.y = 0;
            sunlightRay.z = MathF.Sin(angle);

            return sunlightRay;
        }

        private Vec3 GetBackgroundColor()
        {

            float sunlight = MathF.Abs(Time.Current <= 0.5 ? Time.Current : Time.Current - 1.0f) * 2.0f;
            return new(sunlight, sunlight, sunlight);

        }

    }
}
