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

        public Model skyBox;
        private TextureCube skyBoxTexture;
        private Effect skyBoxEffect;
        private float size = 500f;

        private Matrix world;
        public override void Init()
        {
            world = Matrix.Identity;
            skyBox = Game1.Inst.Content.Load<Model>("Skyboxes/cube");
            skyBoxTexture = Game1.Inst.Content.Load<TextureCube>("Skyboxes/Sunset");
            skyBoxEffect = Game1.Inst.Content.Load<Effect>("Skyboxes/Skybox");
            graphics = Game1.Inst.Graphics;
        }
        public override void Draw(float t, float dt)
        {
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            foreach (CCamera camera in Game1.Inst.Scene.GetComponents<CCamera>().Values)
            {
                // Go through each pass in the effect, but we know there is only one...
                foreach (EffectPass pass in skyBoxEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    // Draw all of the components of the mesh, but we know the cube really
                    // only has one mesh
                    foreach (ModelMesh mesh in skyBox.Meshes)
                    {
                        // Assign the appropriate values to each of the parameters
                        foreach (ModelMeshPart part in mesh.MeshParts)
                        {
                            part.Effect = skyBoxEffect;
                            part.Effect.Parameters["World"].SetValue(
                                Matrix.CreateScale(size) * Matrix.CreateTranslation(camera.CameraPosition));
                            part.Effect.Parameters["View"].SetValue(camera.View);
                            part.Effect.Parameters["Projection"].SetValue(camera.Projection);
                            part.Effect.Parameters["SkyBoxTexture"].SetValue(skyBoxTexture);
                            part.Effect.Parameters["CameraPosition"].SetValue(camera.CameraPosition);
                        }

                        // Draw the mesh with the skybox effect
                        mesh.Draw();
                    }
                }
            }
            graphics.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        }
    }
}