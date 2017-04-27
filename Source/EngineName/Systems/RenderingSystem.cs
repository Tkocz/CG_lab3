using EngineName.Components.Renderable;
using EngineName.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using EngineName.Components;
using Microsoft.Xna.Framework;

namespace EngineName.Systems
{
    public class RenderingSystem : EcsSystem
    {
        private GraphicsDevice mGraphicsDevice;
        private Matrix world;

        public RenderingSystem(Matrix world)
        {
            this.world = world;
        }
        public override void Init()
        {
            mGraphicsDevice = Game1.Inst.GraphicsDevice;
            base.Init();
        }
        public override void Update(float t, float dt)
        {
            base.Update(t, dt);
        }
        public override void Draw(float t, float dt)
        {
            base.Draw(t, dt);

            Game1.Inst.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            int counter = 0;
            foreach (CCamera camera in Game1.Inst.Scene.GetComponents<CCamera>().Values)
            {
                foreach (var component in Game1.Inst.Scene.GetComponents<C3DRenderable>())
                {
                    var key = component.Key;
                    C3DRenderable model = (C3DRenderable)component.Value;
                    if (model.model == null) continue;
                    CTransform transform = (CTransform)Game1.Inst.Scene.GetComponentFromEntity<CTransform>(key);

                    var objectWorld = transform.ObjectWorld;

                    foreach (ModelMesh modelMesh in model.model.Meshes)
                    {
                        foreach (BasicEffect effect in modelMesh.Effects)
                        {
                            effect.World = modelMesh.ParentBone.Transform * objectWorld * world;
                            effect.View = camera.View;
                            effect.Projection = camera.Projection;

                            effect.EnableDefaultLighting();
                            effect.LightingEnabled = true;

                            effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 1f, 1f);
                            effect.DirectionalLight0.Direction = new Vector3(-0.5f, 1f, -3.5f);
                            effect.DirectionalLight0.SpecularColor = new Vector3(-0.1f, -0.1f, -0.1f);

                            foreach (EffectPass p in effect.CurrentTechnique.Passes)
                            {
                                p.Apply();
                                modelMesh.Draw();
                            }
                        }
                    }
                }
            }
        }
    }
}
