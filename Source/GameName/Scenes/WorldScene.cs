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
                new RenderingSystem(Matrix.Identity),
                new TransformSystem(),
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

            AddComponent(camera, new CCamera());
            AddComponent(camera, new CInput());
            AddComponent(camera, new CTransform() { Position = new Vector3(-5, 5, 0), Orientation = Quaternion.Identity, Scale = Vector3.One });

            // Heightmap entity
            int id = AddEntity();
            AddComponent<C3DRenderable>(id, new CHeightmap() { Image = Game1.Inst.Content.Load<Texture2D>("Textures/HeightMap") });
            AddComponent(id, new CTransform() { Position = new Vector3(-590, -100, -590) *0.01f, Orientation = Quaternion.Identity, Scale = new Vector3(1f) });
            // manually start loading all heightmap components, should be moved/automated
            mapSystem.Load();

            Log.Get().Debug("TestScene initialized.");
        }
    }
}
