using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;

namespace Manager.Components
{
    /// <summary>
    /// Component-values, should be made with get-set instead, but, time...
    /// </summary>
    public class CameraComponent : Component
    {
        public Matrix view;
        public Matrix projection;
        public Vector3 up;
		public Vector3 cameraPosition;
		public Vector3 offset;
		public Quaternion cameraRotation;
        public CameraComponent()
        {
            up = Vector3.Up;
			cameraRotation = Quaternion.Identity;
			offset = new Vector3(15, 50, 50);
            view = Matrix.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 0), up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Engine.GetInst().GraphicsDevice.Viewport.AspectRatio, 1f, 400f);
        }
    }
}
