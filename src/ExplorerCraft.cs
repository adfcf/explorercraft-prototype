using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL;
using System.ComponentModel;
using Adfcf.ExplorerCraft.State;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Adfcf.ExplorerCraft.Graphics;
using Adfcf.ExplorerCraft.Graphics.Texture;

namespace Adfcf.ExplorerCraft
{
    internal sealed class ExplorerCraft : GameWindow
    {

        IGameState currentState;

        Renderer renderer;

        public TextureDictionary Textures { get; private set; }

        public ExplorerCraft(GameWindowSettings s1, NativeWindowSettings s2) : base(s1, s2)
        {

            Textures = new();

            currentState = NullState.Instance;
            renderer = new Renderer(this);

        }

        protected override void OnLoad()
        {

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.CullFace(CullFaceMode.Back);

            currentState = new PlayState(this);
            currentState.LoadState();

            Textures.Init();
            renderer.Init();

        }

        int totalFrames;
        double timer;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {

            if (IsKeyDown(Keys.Escape))
            {
                Close();
            }

            timer += args.Time;

            if (timer >= 1) 
            {
                Title = String.Format("RT: {0:f1} ms", (timer * 1000) / totalFrames);
                timer = 0;
                totalFrames = 0;
            }

            currentState.UpdateState(args.Time);

        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            currentState.RenderState(renderer, args.Time);
            SwapBuffers();
            ++totalFrames;
        }

        public void NextState(IGameState gameState)
        {
            currentState.UnloadState();
            currentState = gameState ?? NullState.Instance;
            currentState.LoadState();
        }

        public IGameState GetCurrentState()
        {
            return currentState;
        }

    }
}
