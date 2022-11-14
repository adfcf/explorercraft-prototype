using OpenTK.Graphics.OpenGL;

namespace Adfcf.ExplorerCraft.Graphics.Model
{
    internal class Rect
    {

        readonly int vao;
        readonly int vbo;

        public int Count { get; }

        private Rect()
        {

            float[] vertices = {

                0.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f,
                1.0f, 1.0f, 0.0f,

            };

            Count = 4;

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);

        }

        private static Rect? instance;

        public static Rect Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Rect();
                }
                return instance;
            }
        }
        public void Use()
        {
            GL.BindVertexArray(vao);
        }

    }
}
