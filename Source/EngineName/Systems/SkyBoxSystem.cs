using EngineName.Components;
using EngineName.Components.Renderable;
using EngineName.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineName.Systems
{
    public class SkyBoxSystem : EcsSystem
    {
        private GraphicsDeviceManager graphics;
        private Model skyModel;
        private BasicEffect skyEffect;
        private Matrix viewM, projM, skyworldM, world;
        private static float skyscale = 10000;
        private float slow = skyscale / 200f;  // step width of movements
        private Vector3 nullPos = Vector3.Zero;
        public override void Init()
        {
            world = Matrix.Identity;
            skyModel = Game1.Inst.Content.Load<Model>("Models/skybox");
            skyEffect = (BasicEffect)skyModel.Meshes[0].Effects[0];
            graphics = Game1.Inst.Graphics;
        }
        public override void Update(float t, float dt)
        {
            foreach (var CComponent in Game1.Inst.Scene.GetComponents<CCamera>())
            {
                int id = CComponent.Key;
                CCamera camera = (CCamera)CComponent.Value;
                var transform = (CTransform)Game1.Inst.Scene.GetComponentFromEntity<CTransform>(id);

                var position = transform.Position;
                var view = transform.Scale;

                var scale = transform.Scale;
                var orientation = transform.Orientation;
                var objectWorld = transform.ObjectWorld;
                var elapsedGameTime = t;

                skyworldM = Matrix.CreateScale(skyscale, skyscale, skyscale);
                projM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, 1f, 1f, 10f * skyscale);
                viewM = camera.View;

            }
        }
        public override void Draw(float t, float dt)
        {
            base.Draw(t, dt);

            Game1.Inst.GraphicsDevice.DepthStencilState = DepthStencilState.None;

            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            skyEffect.World = skyworldM;
            skyEffect.View = viewM;
            skyEffect.Projection = projM;
            skyModel.Meshes[0].Draw();
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            Game1.Inst.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        }
    }
}
