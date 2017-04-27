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
