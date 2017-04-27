using EngineName.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineName.Components
{
    public class CCamera : EcsComponent
    {
        public Matrix View;
        public Matrix Projection;
        public Vector3 Up;
        public Vector3 CameraPosition;
        public Vector3 Offset;
        public Quaternion CameraRotation;
        public CCamera()
        {
            Up = Vector3.Up;
            CameraRotation = Quaternion.Identity;
            Offset = new Vector3(0, 50, 50);
            View = Matrix.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 0), Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Game1.Inst.GraphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);
        }
    }
}
