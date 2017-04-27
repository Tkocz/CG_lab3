using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Manager.Subsystems
{
    public class MeshModelSystem : Core
    {
        private Matrix world;
        float modelRotation = 0f;

        public MeshModelSystem(Matrix world)
        {
            this.world = world;
        }

        //Renders models and applies the correct transforms to the models’ submeshes.
        public override void draw(GameTime gameTime)
        {
            Engine.GetInst().GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            CameraComponent playerCam = null;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var cameraComponent = entity.GetComponent<CameraComponent>();
                if (cameraComponent != null)
                    playerCam = cameraComponent;
            }

            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var meshModelComponent = entity.GetComponent<MeshModelComponent>();
                if (meshModelComponent == null)
                    continue;
                var transformComponent = entity.GetComponent<TransformComponent>();

                var objectWorld = transformComponent.objectWorld;

                foreach (ModelMesh modelMesh in meshModelComponent.model.Meshes)
                {
                    foreach (BasicEffect effect in modelMesh.Effects)
                    {
                        effect.World = modelMesh.ParentBone.Transform * objectWorld * world;
                        effect.View = playerCam.view;
                        effect.Projection = playerCam.projection;

                        foreach (Effect e in modelMesh.Effects)
                        {
                            e.CurrentTechnique.Passes[0].Apply();
                            //Engine.GetInst().GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);
                        }
                        modelMesh.Draw();
                    }
                }
            }
        }
        public override void update(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var meshModelComponent = entity.GetComponent<MeshModelComponent>();
                if (meshModelComponent == null)
                    continue;
                if (!meshModelComponent.hasTransformable)
                    continue;
                var tC = entity.GetComponent<TransformComponent>();
                var cameraComponent = entity.GetComponent<CameraComponent>();
                var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                var leftLegMatrix = meshModelComponent.model.Bones["LeftLeg"].Transform;
                var rightLegMatrix = meshModelComponent.model.Bones["RightLeg"].Transform;

                foreach (ModelBone modelBone in meshModelComponent.model.Bones)
                {
                    if (modelRotation < MathHelper.PiOver4)
                    {
                        modelRotation = tC.speed * 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }
                    else
                    {
                        modelRotation = -tC.speed * 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    }

                    if (modelBone.Name == "LeftLeg")
                    {
                        modelBone.Transform = TransformPart(leftLegMatrix, modelRotation);
                    }
                    if (modelBone.Name == "RightLeg")
                    {
                        modelBone.Transform = TransformPart(rightLegMatrix, -modelRotation);
                    }
                }
            }
        }
        private Matrix TransformPart(Matrix origin, float rotation)
        {
            Vector3 originPosition = origin.Translation;

            origin.Translation = Vector3.Zero;
            origin *= Matrix.CreateRotationX(rotation);
            origin.Translation = originPosition;

            return origin;
        }
    }
}