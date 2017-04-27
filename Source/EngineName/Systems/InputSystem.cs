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
        private const float CAMERASPEED = 0.1f;

        public override void Update(float t, float dt){
            KeyboardState currentState = Keyboard.GetState();
            if(currentState.IsKeyDown(Keys.Escape))
                Game1.Inst.Exit();

            foreach (var input in Game1.Inst.Scene.GetComponents<CInput>()) {
                if(Game1.Inst.Scene.EntityHasComponent<CCamera>(input.Key)){
                    var transform = (CTransform)Game1.Inst.Scene.GetComponentFromEntity<CTransform>(input.Key);                
                    inputValue = (CInput)input.Value;
                    CCamera cameraComponent = (CCamera)Game1.Inst.Scene.GetComponentFromEntity<CCamera>(input.Key);
                    
                    if (currentState.IsKeyDown(inputValue.CameraMovementForward)){
                        transform.Position     += CAMERASPEED * new Vector3((float)(cameraComponent.Distance * Math.Sin(cameraComponent.Heading + Math.PI * 0.5f)), 0, (float)((-cameraComponent.Distance) * Math.Cos(cameraComponent.Heading + Math.PI * 0.5f)));
                        cameraComponent.Target += CAMERASPEED * new Vector3((float)(cameraComponent.Distance * Math.Sin(cameraComponent.Heading + Math.PI * 0.5f)), 0, (float)((-cameraComponent.Distance) * Math.Cos(cameraComponent.Heading + Math.PI * 0.5f)));
                    }           
                    if (currentState.IsKeyDown(inputValue.CameraMovementBackward)){
                        transform.Position     -= CAMERASPEED * new Vector3((float)(cameraComponent.Distance * Math.Sin(cameraComponent.Heading + Math.PI * 0.5f)), 0, (float)((-cameraComponent.Distance) * Math.Cos(cameraComponent.Heading + Math.PI * 0.5f)));
                        cameraComponent.Target -= CAMERASPEED * new Vector3((float)(cameraComponent.Distance * Math.Sin(cameraComponent.Heading + Math.PI * 0.5f)), 0, (float)((-cameraComponent.Distance) * Math.Cos(cameraComponent.Heading + Math.PI * 0.5f)));
                    }
                    if (currentState.IsKeyDown(inputValue.CameraMovementLeft)){
                        cameraComponent.Heading -= 0.05f;
                        transform.Position += Vector3.Subtract(cameraComponent.Target, new Vector3((float)(cameraComponent.Distance * Math.Sin(cameraComponent.Heading + Math.PI * 0.5f)), cameraComponent.Height, (float)((-cameraComponent.Distance) * Math.Cos(cameraComponent.Heading + Math.PI * 0.5f))));
                    }       
                    if (currentState.IsKeyDown(inputValue.CameraMovementRight)){
                        cameraComponent.Heading += 0.05f;
                        transform.Position += Vector3.Subtract(cameraComponent.Target, new Vector3((float)(cameraComponent.Distance * Math.Sin(cameraComponent.Heading + Math.PI * 0.5f)), cameraComponent.Height, (float)((-cameraComponent.Distance) * Math.Cos(cameraComponent.Heading + Math.PI * 0.5f))));
                    }
					if (currentState.IsKeyDown(inputValue.CameraMovementUp))
					{
						transform.Position     += CAMERASPEED * 0.5f * new Vector3(0, 1, 0);
						cameraComponent.Target += CAMERASPEED * 0.5f * new Vector3(0, 1, 0);
                    }           
					if (currentState.IsKeyDown(inputValue.CameraMovementDown))
					{
						transform.Position     += CAMERASPEED * 0.5f * new Vector3(0, -1, 0);
						cameraComponent.Target += CAMERASPEED * 0.5f * new Vector3(0, -1, 0);
					}
                    if(currentState.IsKeyDown(inputValue.CameraTiltUp))
					{
						//transform.Position     += CAMERASPEED* 0.5f * new Vector3(0, -1, 0);
						cameraComponent.Target += CAMERASPEED* 0.5f * new Vector3(0, 1, 0);
					}
					if(currentState.IsKeyDown(inputValue.CameraTiltDown))
					{
						//transform.Position     += CAMERASPEED* 0.5f * new Vector3(0, -1, 0);
						cameraComponent.Target += CAMERASPEED* 0.5f * new Vector3(0, -1, 0);
					}
                }                
            }
        }
    }
}
