using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using Adfcf.ExplorerCraft.Mathematics;

namespace Adfcf.ExplorerCraft.Graphics
{
    internal class Camera
    {

        // Directions
        Vec3 up;
        Vec3 right;
        Vec3 director;

        // Points
        Vec3 eye;
        Vec3 target;

        public Matrix4 View
        {
            get
            {
                return Matrix4.LookAt(eye.ToOpenTKFormat(), target.ToOpenTKFormat(), up.ToOpenTKFormat());
            }
        }

        public Matrix4 Projection
        {
            get
            {
                return Matrix4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, 0.1f, 500.0f);
            }
        }


        public float Speed { get; set; } = 0.5f;
        public float Sensibility { get; set; } = 0.015f;
    
        public float FieldOfView { get; set; } = MathHelper.DegreesToRadians(45.0f);
        public float AspectRatio { get; set; } = 4.0f / 3.0f;

        public Camera(Vec3 eye, Vec3 target)
        {

            this.up = new Vec3(0.0f, 1.0f, 0.0f);

            this.eye = eye;
            this.target = target;

            Update();

        }

        public void Update()
        {

            director = target - eye;
            director.Normalize();

            right = Vec3.Cross(director, up);
            right.Normalize();

            //Console.WriteLine("Eye:{0}, Target:{1}, Right:{2})", eye, target, right);

        }

        public void LookAround(float dx, float dy)
        {

            float yaw = dx * Sensibility;
            float pitch = -dy * Sensibility;

            target.Add(right * yaw);
            target.Add(up * pitch);

        }

        public Vec3 GetPosition() => eye;

        public Vec3 GetDirector() => director;

        public void MoveForward()
        {
            eye.Add(director * Speed);
            target.Add(director * Speed);
        }

        public void MoveBackward()
        {
            eye.Add(director * -Speed);
            target.Add(director * -Speed);
        }

        public void MoveLeftward()
        {
            eye.Add(right * -Speed);
            target.Add(right * -Speed);
        }

        public void MoveRightward()
        {
            eye.Add(right * Speed);
            target.Add(right * Speed);
        }

        public void MoveUpward()
        {
            eye.Add(up * Speed);
            target.Add(up * Speed);
        }

        public void MoveDownward()
        {
            eye.Add(up * -Speed);
            target.Add(up * -Speed);
        }

    }
}
