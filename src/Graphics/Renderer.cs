using Adfcf.ExplorerCraft.Graphics.Model;
using Adfcf.ExplorerCraft.Graphics.Shader;
using Adfcf.ExplorerCraft.Graphics.Texture;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using Adfcf.ExplorerCraft.Mathematics;
using Adfcf.ExplorerCraft.World.Blocks;

namespace Adfcf.ExplorerCraft.Graphics
{
    internal class Renderer
    {

        ExplorerCraft Game { get; set; }
        Scheme GraphicScheme { get; }
        ProgramType CurrentProgram { get; set; }
        BatchCube Batch { get; }

        public Renderer(ExplorerCraft game)
        {
            Game = game;
            GraphicScheme = new Scheme();
            CurrentProgram = ProgramType.Block;
            Batch = new BatchCube(1000000);
        }

        public void Init()
        {
            GraphicScheme.LinkPrograms();
            Batch.Generate();
        }

        public void UpdateCamera(Camera camera)
        {
            GraphicScheme.Get(CurrentProgram).Use();
            GraphicScheme.Get(CurrentProgram).UniformMat4("projection", camera.Projection);
            GraphicScheme.Get(CurrentProgram).UniformMat4("view", camera.View);
        }

        public void SetAmbient(Vec3 ambientLight, Vec3 universalDirection)
        {
            GraphicScheme.Get(CurrentProgram).Use();
            GraphicScheme.Get(CurrentProgram).Uniform3("ambientLight", ambientLight.ToOpenTKFormat());
            GraphicScheme.Get(CurrentProgram).Uniform3("universalDirection", universalDirection.ToOpenTKFormat());
        }

        public void SetBackgroundColor(Vec3 color)
        {
            GL.ClearColor(color.x, color.y, color.z, 1.0f);
        }

        public void Begin()
        {
            Game.Textures.Use(TextureDictionary.Type.Blocks);
        }

        public void DrawBlock(BlockDrawingInfo block)
        {
            Batch.Push(block.x, block.y, block.z, Block.GetById(block.id).Texture);
        }

        public void End()
        {

            Batch.Flush();
            Batch.Draw();

            //CurrentProgram = ProgramType.Default;

        }

        public void FinishAll()
        {
            GL.Finish();
        }


    }
}
