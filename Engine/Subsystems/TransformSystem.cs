using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Components;
using Microsoft.Xna.Framework;
using Manager.Helpers;
using Microsoft.Xna.Framework.Input;

namespace Manager.Subsystems
{
    public class TransformSystem : Core
    {
        //Computes the transformation matrices (world-matrices) for all TransformComponents.
        public override void update(GameTime gameTime)
        {


            foreach (var entity in Engine.GetInst().Entities.Values)
			{
				var tC = entity.GetComponent<TransformComponent>();
				if (tC == null)
					continue;
                var scale = tC.scale;
                var orientation = tC.orientation;
                var objectWorld = tC.objectWorld;
                var position = tC.position;
                tC.objectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);
                tC.modelRotation = 0;
                tC.speed = 0;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    tC.speed = 0.001f * (float)gameTime.ElapsedGameTime.Milliseconds;
                    if (tC.modelRotation < MathHelper.PiOver4)
                    {
                        if (tC.direction)
                            tC.modelRotation += tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        else
                            tC.modelRotation -= tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    else
                    {
                        if (!tC.direction)
                            tC.modelRotation -= tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                        else
                            tC.modelRotation += tC.speed * tC.rotationSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }

                    if (tC.modelRotation > MathHelper.PiOver4 || tC.modelRotation < -MathHelper.PiOver4)
                        tC.direction = !tC.direction;
                }
                tC.position.Y = new HeightMapHelper().getHeightMapY(tC.position);
                tC.Rotation *= Quaternion.CreateFromYawPitchRoll(tC.modelRotation, 0, 0);
            }
        }
    }
}
