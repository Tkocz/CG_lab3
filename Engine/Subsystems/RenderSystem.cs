using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Subsystems
{
    public class RenderSystem : Core
    {
        private Matrix world;

        public RenderSystem(Matrix world)
        {
            this.world = world;
        }

        //Renders models and applies the correct transforms to the models’ submeshes.
        public override void draw(GameTime gameTime)
        {
            var modelcount = 0;
            CameraComponent playerCam = null;
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var cameraComponent = entity.GetComponent<CameraComponent>();
                if (cameraComponent != null)
                    playerCam = cameraComponent;
            }

            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var modelComponent = entity.GetComponent<ModelComponent>();
                if (modelComponent == null)
                    continue;
                var transformComponent = entity.GetComponent<TransformComponent>();
                var collisionComponent = entity.GetComponent<CollisionComponent>();
                var objectWorld = transformComponent.objectWorld;

                foreach (ModelMesh modelMesh in modelComponent.model.Meshes)
                {
                    BoundingFrustum cameraFrustrum = new BoundingFrustum(playerCam.view * playerCam.projection);
                    var sphere = collisionComponent.modelBoundingSphere;
                    if (modelComponent.hasTransformable)
                    {
                        sphere.Center = transformComponent.position; ;
                        collisionComponent.modelBoundingSphere = sphere;
                    }
                    if (!cameraFrustrum.Intersects(sphere))
                        continue;
                    modelcount++;
                    foreach (BasicEffect effect in modelMesh.Effects)
                    {
                        effect.World = modelMesh.ParentBone.Transform * objectWorld * world;
                        effect.View = playerCam.view;
                        effect.Projection = playerCam.projection;

                        effect.EnableDefaultLighting();
                        effect.LightingEnabled = true;

                        effect.FogEnabled = true;
                        effect.FogStart = 200;
                        effect.FogEnd = 350;

                        effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f);
                        effect.DirectionalLight0.Direction = new Vector3(-0.5f, 1f, -3.5f);
                        effect.DirectionalLight0.SpecularColor = Color.SlateGray.ToVector3();

                        foreach (EffectPass p in effect.CurrentTechnique.Passes)
                        {
                            p.Apply();
                            modelMesh.Draw();
                        }
                    }
                    var num = Engine.GetInst().Window.Title;
                    Engine.GetInst().Window.Title = "";
                    Engine.GetInst().Window.Title = string.Format("Chunks: {0} Models: {1}", num[8], modelcount);
                    //Utils.DrawSphere(collisionComponent.modelBoundingSphere, collisionComponent.boundColor, Engine.GetInst().GraphicsDevice, new BasicEffect(Engine.GetInst().GraphicsDevice), Matrix.Identity, playerCam.view, playerCam.projection);
                }
            }
        }
    }
}
