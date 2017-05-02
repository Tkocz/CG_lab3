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
            Game1.Inst.GraphicsDevice.Clear(Color.Black);
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
                mapSystem

            );

#if DEBUG
        AddSystem(new DebugOverlay());
#endif

            base.Init();
            // Camera entity
            int camera = AddEntity();

            AddComponent(camera, new CCamera());
            AddComponent(camera, new CInput());
			AddComponent(camera, new CTransform() { Position = new Vector3(0, -6, 1), Orientation = Quaternion.CreateFromRotationMatrix(Matrix.CreateLookAt(new Vector3(0,0,10), new Vector3(0,6,0), Vector3.Up)), Scale = Vector3.One });

            // Heightmap entity
            int id = AddEntity();
            AddComponent<C3DRenderable>(id, new CHeightmap() {  effect = Game1.Inst.Content.Load<Effect>("Fx/Shader"),
                                                                Image = Game1.Inst.Content.Load<Texture2D>("Textures/HeightMap") });
            AddComponent(id, new CTransform() { Position = new Vector3(-590, -900, -590) *0.01f,
                                                Orientation = Quaternion.Identity,
                                                Scale = new Vector3(0.01f) });

            int hangar = AddEntity();
			AddComponent<C3DRenderable>(hangar, new CImportedModel() {  model = Game1.Inst.Content.Load<Model>("Models/moffett-old-building-a"),
                                                                        effect = Game1.Inst.Content.Load<Effect>("Fx/Shader"),
                                                                        texture = Game1.Inst.Content.Load<Texture2D>("Textures/rocktex2") });
            AddComponent(hangar, new CTransform() { Position = new Vector3(0, -895, 0)* 0.01f,
                                                    Orientation = Quaternion.CreateFromRotationMatrix(Matrix.CreateRotationY(MathHelper.PiOver4 * 2)),
                                                    Scale = new Vector3(0.1f) });

            // manually start loading all heightmap components, should be moved/automated
            mapSystem.Load();

            Log.Get().Debug("TestScene initialized.");
        }
    }
}
