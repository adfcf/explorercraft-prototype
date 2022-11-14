using OpenTK.Mathematics;

namespace Adfcf.ExplorerCraft.Mathematics
{
    internal struct Vec4
    {

        public float x;
        public float y;
        public float z;
        public float w;

        public Vec4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public Vec4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vec4(Vec4 vec4)
        {
            this.x = vec4.x;
            this.y = vec4.y;
            this.z = vec4.z;
            this.w = vec4.w;
        }

        public float Length
        {
            get
            {
                return MathF.Sqrt(x*x + y*y + z*z + w*w);
            }
        }

        public float LengthSquared { 
            get { 
                return (x*x + y*y + z*z + w*w); 
            } 
        }

        public void Add(Vec4 v)
        {
            this.x += v.x;
            this.y += v.y;
            this.z += v.z;
            this.w += v.w;
        }

        public void Subtract(Vec4 v)
        {
            this.x += -v.x;
            this.y += -v.y;
            this.z += -v.z;
            this.w += -v.w;
        }

        public void Scale(float scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;
            w *= scalar;
        }

        public void Normalize()
        {

            // float inverseLength = MathF.ReciprocalSqrtEstimate(LengthSquared);
            float inverseLength = 1.0f / Length;

            x *= inverseLength;
            y *= inverseLength;
            z *= inverseLength;
            w *= inverseLength;

        }

        public override string ToString() => string.Format("({0:f2}, {1:f2}, {2:f2}, {3:f2})", x, y, z, w);

        public Vector4 ToOpenTKFormat() => new(x, y, z, w);

        public static Vec4 operator +(Vec4 v, Vec4 u) 
        {
            v.Add(u);
            return v;
        }

        public static Vec4 operator -(Vec4 v, Vec4 u)
        {
            v.Subtract(u);
            return v;
        }

        public static Vec4 operator *(Vec4 v, float scalar)
        {
            v.Scale(scalar);
            return v;
        }

        public static Vec4 operator *(float scalar, Vec4 v)
        {
            v.Scale(scalar);
            return v;
        }



    }
}
