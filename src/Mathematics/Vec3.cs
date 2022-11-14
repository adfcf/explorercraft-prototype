using OpenTK.Mathematics;

namespace Adfcf.ExplorerCraft.Mathematics
{
    internal struct Vec3
    {

        public float x;
        public float y;
        public float z;

        public Vec3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 vec3)
        {
            this.x = vec3.x;
            this.y = vec3.y;
            this.z = vec3.z;
        }

        public float Length
        {
            get
            {
                return MathF.Sqrt(x*x + y*y + z*z);
            }
        }

        public float LengthSquared { 
            get { 
                return (x*x + y*y + z*z); 
            } 
        }

        public void Add(Vec3 v)
        {
            this.x += v.x;
            this.y += v.y;
            this.z += v.z;
        }

        public void Subtract(Vec3 v)
        {
            this.x += -v.x;
            this.y += -v.y;
            this.z += -v.z;
        }

        public void Scale(float scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;
        }

        public void Normalize()
        {

            // float inverseLength = MathF.ReciprocalSqrtEstimate(LengthSquared);
            float inverseLength = 1.0f / Length;

            x *= inverseLength;
            y *= inverseLength;
            z *= inverseLength;

        }

        public override string ToString() => string.Format("({0:f2}, {1:f2}, {2:f2})", x, y, z);

        public Vector3 ToOpenTKFormat() => new(x, y, z);

        public static float Dot(Vec3 v, Vec3 u) => (v.x * u.x + v.y * u.y + v.z * u.z);
        public static Vec3 Cross(Vec3 v, Vec3 u) => new(v.y * u.z - v.z * u.y, u.x * v.z - v.x * u.z, v.x * u.y - u.x * v.y);

        public static Vec3 operator +(Vec3 v, Vec3 u) 
        {
            v.Add(u);
            return v;
        }

        public static Vec3 operator -(Vec3 v, Vec3 u)
        {
            v.Subtract(u);
            return v;
        }

        public static Vec3 operator *(Vec3 v, float scalar)
        {
            v.Scale(scalar);
            return v;
        }

        public static Vec3 operator *(float scalar, Vec3 v)
        {
            v.Scale(scalar);
            return v;
        }



    }
}
