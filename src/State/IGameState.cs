using Adfcf.ExplorerCraft.Graphics;
using OpenTK.Windowing.Common;

namespace Adfcf.ExplorerCraft.State
{
    internal interface IGameState
    {

        void LoadState();
        void UpdateState(double deltaTime);
        void RenderState(Renderer renderer, double deltaTime);
        void UnloadState();

    }
}
