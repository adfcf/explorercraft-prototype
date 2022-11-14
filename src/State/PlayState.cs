using Adfcf.ExplorerCraft.Graphics;
using Adfcf.ExplorerCraft.World;

namespace Adfcf.ExplorerCraft.State
{
    internal class PlayState : IGameState
    {

        ExplorerCraft Game { get; set; }
        GameWorld World { get; set; }

        public PlayState(ExplorerCraft game)
        {
            Game = game;
            World = new(game);

            Game.CursorVisible = false;
            Game.CursorGrabbed = true;

        }

        public void LoadState()
        {
            World.Load();
        }

        public void UpdateState(double deltaTime)
        {
            World.Update(deltaTime);    
        }

        public void RenderState(Renderer renderer, double deltaTime)
        {
            World.Render(renderer, deltaTime);
        }

        public void UnloadState() { }

    }
}
