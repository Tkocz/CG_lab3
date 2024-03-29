﻿using EngineName;
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
                new RenderingSystem(Matrix.Identity),
                new TransformSystem(),
                new CameraSystem(),
                new PhysicsSystem(),
                new InputSystem(),
				new SkyBoxSystem(),
                mapSystem

            );

            base.Init();
            // Camera entity
            int camera = AddEntity();

            AddComponent(camera, new CCamera());
            AddComponent(camera, new CInput());
			AddComponent(camera, new CTransform() { Position = new Vector3(0, -6, 1), Orientation = Quaternion.CreateFromRotationMatrix(Matrix.CreateLookAt(new Vector3(0,0,10), new Vector3(0,6,0), Vector3.Up)), Scale = Vector3.One });

            // Heightmap entity
            int id = AddEntity();
            AddComponent<C3DRenderable>(id, new CHeightmap()
                {   Image = Game1.Inst.Content.Load<Texture2D>("Textures/HeightMap")
                });
            AddComponent(id, new CTransform()
                {   Position = new Vector3(-590, -900, -590) *0.01f,
                    Orientation = Quaternion.Identity,
                    Scale = new Vector3(0.01f)
                });
            //hangar
            int hangar = AddEntity();
			AddComponent<C3DRenderable>(hangar, new CImportedModel()
                {   model = Game1.Inst.Content.Load<Model>("resources/House1Smooth")
            });
            AddComponent(hangar, new CTransform()
                {   Position = new Vector3(0, -855, 0)* 0.01f,
                    Orientation = Quaternion.CreateFromRotationMatrix(Matrix.CreateRotationY(MathHelper.PiOver4 * 2)),
                    Scale = new Vector3(0.005f)
                });
            
            //chopper
            int chopper = AddEntity();
            AddComponent<C3DRenderable>(chopper, new CImportedModel()
                {
                    model = Game1.Inst.Content.Load<Model>("Textures/sphere_mapped"),
                    effect = Game1.Inst.Content.Load<Effect>("Fx/Shader"),
                    texture = Game1.Inst.Content.Load<Texture2D>("Textures/setts"),
                    normalMap = Game1.Inst.Content.Load<Texture2D>("Textures/water-normal-map"),
					environmentMap = new TextureCube(Game1.Inst.GraphicsDevice, 256, true, SurfaceFormat.Color)
					
            });
            AddComponent(chopper, new CTransform()
                {
                    Position = new Vector3(0, -700, 0) * 0.01f,
                    Orientation = Quaternion.CreateFromRotationMatrix(Matrix.CreateRotationY(MathHelper.PiOver4 * 2)),
                    Scale = new Vector3(0.5f)
                });

            // manually start loading all heightmap components, should be moved/automated
            mapSystem.Load();

            Log.Get().Debug("TestScene initialized.");
        }
    }
}
