using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Adfcf.ExplorerCraft.Graphics.Shader.ShaderProgram;

namespace Adfcf.ExplorerCraft.Graphics.Shader
{
    internal class Scheme
    {

        Dictionary<ProgramType, ShaderProgram> programs;

        public Scheme()
        {
            programs = new();
        }

        public void LinkPrograms()
        {
            var fileNames0 = new FileNames("Resources/Shader/DefaultVertexShader.vxsh", "Resources/Shader/DefaultFragmentShader.ftsh");
            var fileNames1 = new FileNames("Resources/Shader/BlockVertexShader.vxsh", "Resources/Shader/BlockFragmentShader.ftsh");
            programs.Add(ProgramType.Default, new(fileNames0));
            programs.Add(ProgramType.Block, new(fileNames1));
        }

        public ShaderProgram Get(ProgramType scheme)
        {
            return programs[scheme];
        }

    }

    internal enum ProgramType
    {
        Default,
        Block
    }

}
