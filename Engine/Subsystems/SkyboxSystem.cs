using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Manager.Subsystems
{
    public class SkyboxSystem : Core
    {
        private GraphicsDeviceManager graphics;
        private Model skyModel;
        private BasicEffect skyEffect;
        private Matrix viewM, projM, skyworldM, world;
        private static float skyscale = 1000;
        private float slow = skyscale / 200f;  // step width of movements
        private Vector3 nullPos = Vector3.Zero;
        public SkyboxSystem(Matrix world)
        {
            this.world = world;
            skyModel = Engine.GetInst().Content.Load<Model>("skybox");
            skyEffect = (BasicEffect)skyModel.Meshes[0].Effects[0];
            graphics = Engine.GetInst().graphics;
        }
        public override void update(GameTime gameTime)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var transformComponent = entity.GetComponent<TransformComponent>();
                var cameraComponent = entity.GetComponent<CameraComponent>();
                if (transformComponent == null || cameraComponent == null)
                    continue;
				skyworldM = Matrix.CreateScale(skyscale, skyscale, skyscale) * Matrix.CreateTranslation(1081f / 2, 0, -1081f / 2);// * Matrix.Identity;
                projM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 3, 1f, 1f, 5f * skyscale);
                viewM = cameraComponent.view;
            }
        }
        public override void draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
			RasterizerState rs = new RasterizerState();
			rs.CullMode = CullMode.None;
			graphics.GraphicsDevice.RasterizerState = rs;
            skyEffect.World = skyworldM;
            skyEffect.View = viewM;
            skyEffect.Projection = projM;
            skyModel.Meshes[0].Draw();
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }
    }
}
