using Manager;
using Manager.Components;
using Manager.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Manager.Subsystems
{
    public class ModelSystem : Core
    {
        private Matrix world;

        public ModelSystem(Matrix world)
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
						sphere.Center = transformComponent.position;;
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
        public override void update(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var modelComponent = entity.GetComponent<ModelComponent>();
				if (modelComponent == null)
					continue;
                if (!modelComponent.hasTransformable)
                    continue;
    
                var elapsedGameTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                foreach (ModelBone modelBone in modelComponent.model.Bones)
                {
                    if(modelBone.Name == "Main_Rotor") //Non-generic solution, but it works ¯\_(ツ)_/
                    {
                        Matrix MainRotorWorldMatrix;
                        MainRotorWorldMatrix = modelBone.Transform;
                        MainRotorWorldMatrix *= Matrix.CreateTranslation(-modelBone.Transform.Translation); 
                        MainRotorWorldMatrix *= Matrix.CreateRotationY(elapsedGameTime * 0.5f);
                        MainRotorWorldMatrix *= Matrix.CreateTranslation(modelBone.Transform.Translation);  
                        modelBone.Transform = MainRotorWorldMatrix;
                    }
                    if (modelBone.Name == "Back_Rotor") //Non-generic solution, but it works ¯\_(ツ)_/
                    {
                        Matrix BackRotorWorldMatrix;
                        BackRotorWorldMatrix = modelBone.Transform;
                        BackRotorWorldMatrix *= Matrix.CreateTranslation(-modelBone.Transform.Translation);
                        BackRotorWorldMatrix *= Matrix.CreateRotationX(elapsedGameTime * 2f);
                        BackRotorWorldMatrix *= Matrix.CreateTranslation(modelBone.Transform.Translation);        
                        modelBone.Transform = BackRotorWorldMatrix;
                    }
                }
            }
        }
    }
}
