using OpenTK.Mathematics;
using Adfcf.ExplorerCraft.Graphics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using Adfcf.ExplorerCraft.World;
using Adfcf.ExplorerCraft.Mathematics;

namespace Adfcf.ExplorerCraft
{
    internal class Player
    {

        public Camera FirstCamera { get; private set; }
        public ExplorerCraft Game { get; private set; }

        GameWorld world;

        public Player(ExplorerCraft game, GameWorld world, Vec3 position)
        {

            this.world = world;

            Game = game;
            FirstCamera = new(position, position + new Vec3(0.0f, 0.0f, 1.0f));

        }

        public void Load()
        {
            Game.MouseMove += ProcessMouseMoveEvent;
            Game.MouseDown += ProcessMouseButtonEvent;
        }

        public  void Update(double deltaTime)
        {

            ProcessInput();

            FirstCamera.LookAround(dx, dy);
            dx = 0; dy = 0;

            FirstCamera.Update();

        }

        private void ProcessInput()
        {

            var keyboard = Game.KeyboardState;

            if (keyboard.IsKeyDown(Keys.W))
            {
                FirstCamera.MoveForward();
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                FirstCamera.MoveBackward();
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                FirstCamera.MoveLeftward();
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                FirstCamera.MoveRightward();
            }

            if (keyboard.IsKeyDown(Keys.Space))
            {
                FirstCamera.MoveUpward();
            }
            else if (keyboard.IsKeyDown(Keys.LeftShift))
            {
                FirstCamera.MoveDownward();
            }
        }

        bool firstMouse = true;
        float lastMouseX, lastMouseY;
        float dx, dy;
        private void ProcessMouseMoveEvent(MouseMoveEventArgs args)
        {

            if (firstMouse)
            {
                firstMouse = false;
                lastMouseX = args.X;
                lastMouseY = args.Y;
            }

            dx += args.X - lastMouseX;
            dy += args.Y - lastMouseY;

            lastMouseX = args.X;
            lastMouseY = args.Y;

        }

        private void ProcessMouseButtonEvent(MouseButtonEventArgs args)
        {
            if (args.Button == MouseButton.Left && args.IsPressed)
            {
                world.ChunkManager.DestroyBlock();
            }
        }

        public void Draw(Renderer renderer, double deltaTime)
        {
            
        }

        public bool CanSee(Chunk chunk)
        {

            bool tooFar;
            bool closeEnough;
            bool behind;

            var pos = FirstCamera.GetPosition();

            float dx = pos.x - chunk.CenterX();
            float dy = pos.y - chunk.CenterY();
            float dz = pos.z - chunk.CenterZ();

            float distance = MathF.Sqrt(dx * dx + dy * dy + dz * dz);

            tooFar = distance > 256;
            closeEnough = distance < 64;

            Vec3 director = FirstCamera.GetDirector();
            Vec3 cpos = new Vec3(chunk.CenterX(), chunk.CenterY(), chunk.CenterZ()) - FirstCamera.GetPosition();

            float angle = MathHelper.RadiansToDegrees(MathF.Acos((Vec3.Dot(director, cpos)) / (director.Length * cpos.Length)));
            behind = angle > 90;

            if ((tooFar || behind) && !closeEnough)
                return false;

            return true;

        }

    }
}
