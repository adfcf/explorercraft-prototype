using System;
using System.IO;

using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace Adfcf.ExplorerCraft.Graphics.Shader
{
    internal class ShaderProgram
    {

        private int Handle { get; set; }

        public ShaderProgram(Sources sources)
        {

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);

            GL.ShaderSource(vertexShader, sources.VertexShaderSource);
            GL.CompileShader(vertexShader);

            string vertexShaderInfoLog = GL.GetShaderInfoLog(vertexShader);
            if (vertexShaderInfoLog != String.Empty)
                Console.WriteLine("COMPILATION_ERROR:VERTEX_SHADER:\n{0}", vertexShaderInfoLog);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            
            GL.ShaderSource(fragmentShader, sources.FragmentShaderSource);
            GL.CompileShader(fragmentShader);

            string fragmentShaderInfoLog = GL.GetShaderInfoLog(fragmentShader);
            if (fragmentShaderInfoLog != String.Empty)
                Console.WriteLine("COMPILATION_ERROR:FRAGMENT_SHADER:\n{0}", fragmentShaderInfoLog);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);

            string programInfoLog = GL.GetProgramInfoLog(Handle);
            if (programInfoLog != String.Empty)
                Console.WriteLine("LINK_ERROR:SHADER_PROGRAM:\n{0}", programInfoLog);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.UseProgram(Handle);
            GL.UseProgram(0);

        }

        public ShaderProgram(FileNames fileNames) : this(ReadShaderFiles(fileNames)) { }

        private static Sources ReadShaderFiles(FileNames fileNames)
        {

            using var vsReader = new StreamReader(fileNames.VertexShaderFileName);
            using var fsReader = new StreamReader(fileNames.FragmentShaderFileName);

            string vertexShaderSource = vsReader.ReadToEnd();
            string fragmentShaderSource = fsReader.ReadToEnd();

            return new Sources(vertexShaderSource, fragmentShaderSource);

        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        public void UniformMat4(string uniform, Matrix4 matrix)
        {
            GL.UniformMatrix4(GetLocation(uniform), false, ref matrix);
        }

        public void UniformMat3(string uniform, Matrix3 matrix)
        {
            GL.UniformMatrix3(GetLocation(uniform), false, ref matrix);
        }

        public void Uniform4(string uniform, Vector4 vector)
        {
            GL.Uniform4(GetLocation(uniform), ref vector);
        }

        public void Uniform3(string uniform, Vector3 vector)
        {
            GL.Uniform3(GetLocation(uniform), ref vector);
        }

        public void Uniform2(string uniform, Vector2 vector)
        {
            GL.Uniform2(GetLocation(uniform), ref vector);
        }

        public void Uniform1i(string uniform, int i0)
        {
            GL.Uniform1(GetLocation(uniform), i0);
        }

        private int GetLocation(string uniformName)
        {
            return GL.GetUniformLocation(Handle, uniformName);
        }

        public struct FileNames
        {
            public string VertexShaderFileName { get; }
            public string FragmentShaderFileName { get; }

            public FileNames(string vertexShaderFileName, string fragmentShaderFileName)
            {
                VertexShaderFileName = vertexShaderFileName;
                FragmentShaderFileName = fragmentShaderFileName;
            }

        }

        public struct Sources
        {
            public string VertexShaderSource { get; }
            public string FragmentShaderSource { get; }

            public Sources(string vertexShaderSource, string fragmentShaderSource)
            {
                VertexShaderSource = vertexShaderSource;
                FragmentShaderSource = fragmentShaderSource;
            }

        }

    }
}
