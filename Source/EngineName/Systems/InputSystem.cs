using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EngineName.Components.Renderable;
using EngineName.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineName.Utils;
using EngineName.Components;

namespace EngineName.Systems {
    public class InputSystem : EcsSystem {
        private CInput inputValue = null;
        private const float CAMERASPEED = 0.001f;

        public override void Update(float t, float dt){
            KeyboardState currentState = Keyboard.GetState();
            if(currentState.IsKeyDown(Keys.Escape))
                Game1.Inst.Exit();

            foreach (var input in Game1.Inst.Scene.GetComponents<CInput>()) {
                if (Game1.Inst.Scene.EntityHasComponent<CCamera>(input.Key))
                {
                    var transform = (CTransform)Game1.Inst.Scene.GetComponentFromEntity<CTransform>(input.Key);
                    inputValue = (CInput)input.Value;
                    CCamera cameraComponent = (CCamera)Game1.Inst.Scene.GetComponentFromEntity<CCamera>(input.Key);

                    if (currentState.IsKeyDown(inputValue.CameraMovementForward))
                    {
                        transform.Position += CAMERASPEED * t * transform.ObjectWorld.Forward;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraMovementBackward))
                    {
                        transform.Position += CAMERASPEED * t * transform.ObjectWorld.Backward;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraMovementLeft))
                    {
                        transform.Position += CAMERASPEED * t * transform.ObjectWorld.Left;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraMovementRight))
                    {
                        transform.Position += CAMERASPEED * t * transform.ObjectWorld.Right;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraMovementUp))
                    {
                        transform.Position += CAMERASPEED * 1f * new Vector3(0, 1, 0);
                    }
                    if (currentState.IsKeyDown(inputValue.CameraMovementDown))
                    {
                        transform.Position += CAMERASPEED * 1f * new Vector3(0, -1, 0);
                    }


					transform.YPR = Vector3.Zero;
                    float angle = t * 0.001f;

                    if (currentState.IsKeyDown(inputValue.CameraTiltUp))
                    {
						transform.YPR.Y += angle;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraTiltDown))
                    {
                        transform.YPR.Y += -angle;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraTiltLeft))
                    {
                        transform.YPR.X += angle;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraTiltRight))
                    {
                        transform.YPR.X += -angle;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraTiltRollLeft))
                    {
                        transform.YPR.Z += angle;
                    }
                    if (currentState.IsKeyDown(inputValue.CameraTiltRollRight))
                    {
                        transform.YPR.Z += -angle;
                    }
                }
            }
        }
    }
}
