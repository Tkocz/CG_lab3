using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Subsystems
{
    public class InputSystem : Core
    {
        public override void update(GameTime gameTime)
        {
            float elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var tC = entity.GetComponent<TransformComponent>();
                var userInput = entity.GetComponent<InputComponent>();
                if (tC == null || userInput == null)
                    continue;

                if (Keyboard.GetState().IsKeyDown(userInput.a))
                    tC.position += tC.speed.X * elapsedGameTime * tC.objectWorld.Left;

                if (Keyboard.GetState().IsKeyDown(userInput.d))
                    tC.position += tC.speed.X * elapsedGameTime * tC.objectWorld.Right;

                if (Keyboard.GetState().IsKeyDown(userInput.w))
                    tC.position += tC.speed.Z * elapsedGameTime * tC.objectWorld.Forward;

                if (Keyboard.GetState().IsKeyDown(userInput.s))
                    tC.position += tC.speed.Z * elapsedGameTime * tC.objectWorld.Backward;

                if (Keyboard.GetState().IsKeyDown(userInput.space))
                    tC.position += tC.speed.Y * elapsedGameTime * tC.objectWorld.Up;

                if (Keyboard.GetState().IsKeyDown(userInput.lShift))
                    tC.position += tC.speed.Y * elapsedGameTime * tC.objectWorld.Down;

                Quaternion addRot;
                float yaw = 0, pitch = 0, roll = 0;
                float angle = elapsedGameTime * 0.001f;

                if (Keyboard.GetState().IsKeyDown(userInput.left))
                {
                    yaw = angle;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.right))
                {
                    yaw = -angle;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.up))
                {
                    pitch = -angle;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.down))
                {
                    pitch = angle;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.q))
                {
                    roll = -angle;
                }

                if (Keyboard.GetState().IsKeyDown(userInput.e))
                {
                    roll = angle;
                }

                addRot = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);

                addRot.Normalize();
                tC.orientation *= addRot;


                // Reset to original (zero) rotation
                if (Keyboard.GetState().IsKeyDown(userInput.r))
                    tC.orientation = Quaternion.Identity;
            }
        }
    }
}
