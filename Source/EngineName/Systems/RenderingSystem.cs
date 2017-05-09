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
using System.IO;

namespace EngineName.Systems
{
	public class RenderingSystem : EcsSystem
	{
		private GraphicsDevice mGraphicsDevice;
		private Matrix world;
		private Vector3 viewVector;
		private TextureCube skyBoxTexture;

		public RenderingSystem(Matrix world)
		{
			this.world = world;
		}
		public override void Init()
		{
			mGraphicsDevice = Game1.Inst.GraphicsDevice;
			skyBoxTexture = Game1.Inst.Content.Load<TextureCube>("Skyboxes/Sunset");
			base.Init();
		}
		public override void Update(float t, float dt)
		{
			foreach (CCamera camera in Game1.Inst.Scene.GetComponents<CCamera>().Values)
			{
				viewVector = Vector3.Transform(camera.Offset - camera.CameraPosition, Matrix.CreateRotationY(0));
				viewVector.Normalize();
			}
			base.Update(t, dt);
		}
		public override void Draw(float t, float dt)
		{
			base.Draw(t, dt);
			createEnvMapping();
			drawNormal();
		}

		public void drawNormal()
		{
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
						{
							if (model.effect != null)
							{
								foreach (ModelMeshPart part in modelMesh.MeshParts)
								{
									part.Effect = model.effect;
									part.Effect.Parameters["World"].SetValue(modelMesh.ParentBone.Transform * objectWorld * world);
									part.Effect.Parameters["View"].SetValue(camera.View);
									part.Effect.Parameters["Projection"].SetValue(camera.Projection);
									part.Effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector3());
									part.Effect.Parameters["AmbientIntensity"].SetValue(0.1f);
									part.Effect.Parameters["LightDirection"].SetValue(new Vector3(0, 1f, 0));
									part.Effect.Parameters["SpecularPower"].SetValue(400f);
									part.Effect.Parameters["SpecularColor"].SetValue(Color.Gray.ToVector3());
									part.Effect.Parameters["SpecularIntensity"].SetValue(3.0f);
									part.Effect.Parameters["ViewVector"].SetValue(viewVector);
									part.Effect.Parameters["ModelTexture"].SetValue(model.texture);
									part.Effect.Parameters["BumpConstant"].SetValue(1f);
									part.Effect.Parameters["NormalMap"].SetValue(model.normalMap);
									part.Effect.Parameters["EnvTexture"].SetValue(model.environmentMap);
								}
								modelMesh.Draw();

							}
							if (model.effect == null)
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

		private void createEnvMapping()
		{
			C3DRenderable model = null;
			foreach (var component in Game1.Inst.Scene.GetComponents<C3DRenderable>())
			{
				var key = component.Key;
				model = (C3DRenderable)component.Value;
				if (model.effect == null) continue;
			}
			RenderTarget2D renderTarget = new RenderTarget2D(Game1.Inst.GraphicsDevice, 256, 256);
			//Game1.Inst.GraphicsDevice.Clear(Color.Black);
			Matrix view;
			for (int i = 0; i < 6; i++)
			{
				// render the scene to all cubemap faces
				CubeMapFace cubeMapFace = (CubeMapFace)i;

				switch (cubeMapFace)
				{
					case CubeMapFace.NegativeX:
						{
							view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Left, Vector3.Up);
							break;
						}
					case CubeMapFace.NegativeY:
						{
							view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Down, Vector3.Forward);
							break;
						}
					case CubeMapFace.NegativeZ:
						{
							view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Backward, Vector3.Up);
							break;
						}
					case CubeMapFace.PositiveX:
						{
							view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Right, Vector3.Up);
							break;
						}
					case CubeMapFace.PositiveY:
						{
							view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Up, Vector3.Backward);
							break;
						}
					case CubeMapFace.PositiveZ:
						{
							view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
							break;
						}
				}

				// Set the cubemap render target, using the selected face
				Game1.Inst.GraphicsDevice.SetRenderTarget(renderTarget);
				//Game1.Inst.GraphicsDevice.Clear(Color.White);
				drawNormal();
				Color[] colordata = new Color[256 * 256];
				renderTarget.GetData(colordata);
				model.environmentMap.SetData(cubeMapFace, colordata);
				//DEBUG
				Stream stream = File.Create("test" + i + ".jpg");
				renderTarget.SaveAsJpeg(stream, 256, 256);
				stream.Dispose();
				// restore our matrix after changing during the cube map rendering
				view = Matrix.CreateLookAt(new Vector3(0, 0, 80), Vector3.Zero, Vector3.Up);

			}
			Game1.Inst.GraphicsDevice.SetRenderTarget(null);
		}
	}
}
  

