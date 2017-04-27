using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Helpers
{
    public class CubeModel
    {
        public Model CreateCubeModel(String name)
        {
            var body = new CubeMesh();
            var lLeg = new CubeMesh();
            var rLeg = new CubeMesh();
            var meshPartBody = new ModelMeshPart();
            var meshPartLLeg = new ModelMeshPart();
            var meshPartRLeg = new ModelMeshPart();

            meshPartBody.IndexBuffer = body.indexBuffer;
            meshPartBody.VertexBuffer = body.vertexBuffer;
            meshPartBody.PrimitiveCount = body.vertices.Length / 3;
            meshPartBody.NumVertices = body.vertices.Length;

            meshPartLLeg.IndexBuffer = lLeg.indexBuffer;
            meshPartLLeg.VertexBuffer = lLeg.vertexBuffer;
            meshPartLLeg.PrimitiveCount = lLeg.vertices.Length / 3;
            meshPartLLeg.NumVertices = lLeg.vertices.Length;

            meshPartRLeg.IndexBuffer = rLeg.indexBuffer;
            meshPartRLeg.VertexBuffer = rLeg.vertexBuffer;
            meshPartRLeg.PrimitiveCount = rLeg.vertices.Length / 3;
            meshPartRLeg.NumVertices = rLeg.vertices.Length;

            List<ModelMeshPart> meshBodyList = new List<ModelMeshPart>();
            meshBodyList.Add(meshPartBody);
            List<ModelMeshPart> meshRLegList = new List<ModelMeshPart>();
            meshRLegList.Add(meshPartRLeg);
            List<ModelMeshPart> meshLLegList = new List<ModelMeshPart>();
            meshLLegList.Add(meshPartLLeg);

            var meshRLeg = new ModelMesh(Engine.GetInst().GraphicsDevice, meshRLegList);
            var meshLLeg = new ModelMesh(Engine.GetInst().GraphicsDevice, meshLLegList);
            var meshBody = new ModelMesh(Engine.GetInst().GraphicsDevice, meshBodyList);

            var boneBody = new ModelBone();
            boneBody.Index = 0;
            boneBody.Name = name;
            boneBody.Transform = Matrix.Identity * Matrix.CreateTranslation(0f, 1f, 0f);
            boneBody.ModelTransform = Matrix.Identity;
            boneBody.AddMesh(meshBody);
            meshBody.ParentBone = boneBody;

            var boneRLeg = new ModelBone();
            boneRLeg.Index = 1;
            boneRLeg.Name = "RightLeg";
            boneRLeg.Transform = Matrix.Identity * Matrix.CreateTranslation(2f, -1f, 0) * Matrix.CreateScale(0.3f, 1.5f, 0.3f) * Matrix.CreateRotationX((float)Math.PI);
            boneRLeg.ModelTransform = Matrix.Identity;
            boneRLeg.AddMesh(meshRLeg);
            meshRLeg.ParentBone = boneRLeg;

            var boneLLeg = new ModelBone();
            boneLLeg.Index = 2;
            boneLLeg.Name = "LeftLeg";
            boneLLeg.Transform = Matrix.Identity * Matrix.CreateTranslation(-2f, -1f, 0) * Matrix.CreateScale(0.3f, 1.5f, 0.3f) * Matrix.CreateRotationX((float)Math.PI);
            boneLLeg.ModelTransform = Matrix.Identity;
            boneLLeg.AddMesh(meshLLeg);
            meshLLeg.ParentBone = boneLLeg;

            List<ModelBone> modelBones = new List<ModelBone>();
            modelBones.Add(boneBody);
            modelBones.Add(boneRLeg);
            modelBones.Add(boneLLeg);

            List<ModelMesh> modelMeshes = new List<ModelMesh>();
            modelMeshes.Add(meshBody);
            modelMeshes.Add(meshRLeg);
            modelMeshes.Add(meshLLeg);

            meshBodyList.ForEach((ModelMeshPart obj) => {
                var effect = new BasicEffect(Engine.GetInst().GraphicsDevice)
                {
                    //Texture = Engine.GetInst().Content.Load<Texture2D>("das_robot"),
                    //TextureEnabled = true,
                    VertexColorEnabled = true
                };
                obj.Effect = effect;
            });
            meshRLegList.ForEach((ModelMeshPart obj) => {
                var effect = new BasicEffect(Engine.GetInst().GraphicsDevice)
                {
                    VertexColorEnabled = true
                };
                obj.Effect = effect;
            });
            meshLLegList.ForEach((ModelMeshPart obj) => {
                var effect = new BasicEffect(Engine.GetInst().GraphicsDevice)
                {
                    VertexColorEnabled = true
                };
                obj.Effect = effect;
            });

            Model m = new Model(Engine.GetInst().GraphicsDevice, modelBones, modelMeshes);

            return m;
        }
    }
}