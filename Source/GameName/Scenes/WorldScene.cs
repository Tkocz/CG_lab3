using EngineName;
using EngineName.Components;
using EngineName.Components.Renderable;
using EngineName.Logging;
using EngineName.Systems;
using EngineName.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameName.Scenes
{
    public class WorldScene : Scene
    {
        public override void Draw(float t, float dt) {
            Game1.Inst.GraphicsDevice.Clear(Color.Aqua);
            base.Draw(t, dt);
        }

        public override void Init() {

            var mapSystem = new MapSystem();
            AddSystems(
                new SkyBoxSystem(),
                new RenderingSystem(),
                new CameraSystem(),
                new PhysicsSystem(),
                new InputSystem(),
                mapSystem,
                new Rendering2DSystem()

            );

#if DEBUG
        AddSystem(new DebugOverlay());
#endif

            base.Init();
            // Camera entity
            int camera = AddEntity();
            float fieldofview = MathHelper.PiOver2;
            float nearplane = 0.1f;
            float farplane = 1000f;

            AddComponent(camera, new CCamera(-5, 5){
                Projection = Matrix.CreatePerspectiveFieldOfView(fieldofview, Game1.Inst.GraphicsDevice.Viewport.AspectRatio,nearplane,farplane)
                ,ClipProjection = Matrix.CreatePerspectiveFieldOfView(fieldofview*1.2f, Game1.Inst.GraphicsDevice.Viewport.AspectRatio, nearplane*0.5f, farplane*1.2f)
            });
            AddComponent(camera, new CInput());
            AddComponent(camera, new CTransform() { Position = new Vector3(-5, 5, 0), Rotation = Matrix.Identity, Scale = Vector3.One });

            // Heightmap entity
            int id = AddEntity();
            AddComponent<C3DRenderable>(id, new CHeightmap() { Image = Game1.Inst.Content.Load<Texture2D>("Textures/HeightMap") });
            AddComponent(id, new CTransform() { Position = new Vector3(-590, -900, -590) *0.01f, Rotation = Matrix.Identity, Scale = new Vector3(0.01f) });
            // manually start loading all heightmap components, should be moved/automated
            mapSystem.Load();

            Log.Get().Debug("TestScene initialized.");
        }
    }
}
