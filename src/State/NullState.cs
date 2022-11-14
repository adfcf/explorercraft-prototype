using Adfcf.ExplorerCraft.Graphics;
using OpenTK.Windowing.Common;
using System;

namespace Adfcf.ExplorerCraft.State
{
    internal class NullState : IGameState
    {

        private NullState() { }

        public static NullState Instance { get; } = new();

        public void LoadState() { }
        public void UpdateState(double dt) { }
        public void RenderState(Renderer renderer, double dt) { }
        public void UnloadState() { }

    }
}
