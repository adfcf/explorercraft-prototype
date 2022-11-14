using Adfcf.ExplorerCraft.Mathematics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Adfcf.ExplorerCraft.Graphics.Model
{
    internal class BatchCube
    {

        readonly int InstanceDataElements = 7;

        int vao;
        int ebo;
        int modelVbo;
        int instanceVbo;

        public int Capacity { get; }

        readonly float[] instanceData;

        int currentIndex;
        int pushedElements;
        int toDraw;

        public BatchCube(int capacity)
        {
            Capacity = capacity;
            instanceData = new float[capacity * InstanceDataElements];
        }

        public void Generate()
        {

            // PPP-NNN-UU-N-TTT-FFFF

            float[] vertices = {

                // Front
                0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0, // 0
                1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f, 0, // 1
                1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 0, // 2
                0.0f, 1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0, // 3

                // Left
                1.0f, 0.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1, // 4
                1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1, // 5
                1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1, // 6
                1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1, // 7

                // Back
                1.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 1, // 8
                0.0f, 0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 1.0f, 0.0f, 1, // 9
                0.0f, 1.0f, 0.0f, 0.0f, 0.0f, -1.0f, 1.0f, 1.0f, 1, // 10
                1.0f, 1.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f, 1, // 11

                // Right
                0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1, // 12
                0.0f, 0.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1, // 13
                0.0f, 1.0f, 1.0f, -1.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1, // 14
                0.0f, 1.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 1, // 15

                // Up
                0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 2, // 16
                1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 2, // 17
                1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 2, // 18
                0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 2, // 19

                // Down
                0.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 0.0f, 0.0f, 3, // 20
                1.0f, 0.0f, 0.0f, 0.0f, -1.0f, 0.0f, 1.0f, 0.0f, 3, // 21
                1.0f, 0.0f, 1.0f, 0.0f, -1.0f, 0.0f, 1.0f, 1.0f, 3, // 22
                0.0f, 0.0f, 1.0f, 0.0f, -1.0f, 0.0f, 0.0f, 1.0f, 3  // 23

            };

            int[] indices =
            {
                0, 1, 2, 2, 3, 0,
                4, 5, 6, 6, 7, 4,
                8, 9, 10, 10, 11, 8,
                12, 13, 14, 14, 15, 12,
                16, 17, 18, 18, 19, 16,
                20, 21, 22, 22, 23, 20
            };

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            modelVbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, modelVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length, vertices, BufferUsageHint.StaticDraw);

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indices.Length, indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 9 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(3, 1, VertexAttribPointerType.Float, false, 9 * sizeof(float), 8 * sizeof(float));
            GL.EnableVertexAttribArray(3);

            instanceVbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, instanceVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, InstanceDataElements * Capacity * sizeof(float), IntPtr.Zero, BufferUsageHint.DynamicDraw);

            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0 * sizeof(float));
            GL.EnableVertexAttribArray(4);
            GL.VertexAttribDivisor(4, 1);

            GL.VertexAttribPointer(5, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(5);
            GL.VertexAttribDivisor(5, 1);

            GL.BindVertexArray(0);
        }

        public void Push(float tx, float ty, float tz, Vec4 texture)
        {

            if (pushedElements >= Capacity)
                return;

            instanceData[currentIndex++] = tx;
            instanceData[currentIndex++] = ty;
            instanceData[currentIndex++] = tz;

            instanceData[currentIndex++] = texture.x;
            instanceData[currentIndex++] = texture.y;
            instanceData[currentIndex++] = texture.z;
            instanceData[currentIndex++] = texture.w;

            ++pushedElements;

        }

        public void Flush()
        {

            if (pushedElements == 0)
                return;

            GL.BindBuffer(BufferTarget.ArrayBuffer, instanceVbo);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, pushedElements * InstanceDataElements * sizeof(float), instanceData);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            toDraw = pushedElements;

            currentIndex = 0;
            pushedElements = 0;

        }

        public void Draw()
        {
            GL.BindVertexArray(vao);
            GL.DrawElementsInstanced(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedInt, IntPtr.Zero, toDraw);
        }

    }
}
