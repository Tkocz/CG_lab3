using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Manager;
using Manager.Components;

namespace Manager.Subsystems
{
    public class CameraSystem : Core
    {
        //Computes the view and projection matrix for all the CameraComponents.
        public override void update(GameTime gameTime)
        {
            CameraComponent camera = null;
            TransformComponent transform = null;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var cameraModel = entity.GetComponent<CameraComponent>();
                if (cameraModel != null)
                {
                    camera = cameraModel;
                    transform = entity.GetComponent<TransformComponent>();
                }
            }

			var cameraRotation = Quaternion.Lerp(camera.cameraRotation, transform.orientation, 0.1f);

			Vector3 cameraPosition = Vector3.Transform(camera.offset, transform.orientation);
			cameraPosition += transform.position;

			Vector3 cameraUp = new Vector3(0, 1, 0);
			cameraUp = Vector3.Transform(cameraUp, transform.orientation);

			camera.view = Matrix.CreateLookAt(cameraPosition, transform.position, cameraUp);

			camera.cameraRotation = cameraRotation;
            camera.up = cameraUp;
        }
    }
}
