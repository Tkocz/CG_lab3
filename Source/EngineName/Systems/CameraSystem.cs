using EngineName.Components;
using EngineName.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineName.Systems
{
    public class CameraSystem : EcsSystem {
        private GraphicsDevice mGraphicsDevice;
		float angle = 0;
		Vector3 viewVector;
		Vector3 cameraLocation;
float distance = 10;
        public override void Init() {
            mGraphicsDevice = Game1.Inst.GraphicsDevice;
            base.Init();
        }
        public override void Update(float t, float dt)
        {
            base.Update(t, dt);

            foreach (var CComponent in Game1.Inst.Scene.GetComponents<CCamera>())
            {
                int id = CComponent.Key;
                CCamera camera = (CCamera)CComponent.Value;
                var transform = (CTransform)Game1.Inst.Scene.GetComponentFromEntity<CTransform>(id);
                var cameraRotation = Quaternion.Lerp(camera.CameraRotation, transform.Orientation, 0.1f);

                Vector3 cameraPosition = Vector3.Transform(camera.Offset, transform.Orientation);
                cameraPosition += transform.Position;

                Vector3 cameraUp = new Vector3(0, 1, 0);
                cameraUp = Vector3.Transform(cameraUp, transform.Orientation);

                camera.View = Matrix.CreateLookAt(cameraPosition, transform.Position, cameraUp);

                camera.CameraRotation = cameraRotation;
                camera.Up = cameraUp;
            }
        }
    }
}
