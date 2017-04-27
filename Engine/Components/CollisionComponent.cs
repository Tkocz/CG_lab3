using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Manager.Core;

namespace Manager.Components
{
	public class CollisionComponent : Component
	{
		public BoundingSphere modelBoundingSphere { get; set; }
		public Color boundColor { get; set; }
		public Matrix objectWorld;

		public CollisionComponent(ModelComponent comp, TransformComponent transform)
		{
			Model model = comp.model;
			Vector3 trans;
			Vector3 scaling;
			Quaternion rot;
			Random rand = new Random();
			this.boundColor = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
			Matrix[] modelTransforms = new Matrix[model.Bones.Count];
			model.CopyAbsoluteBoneTransformsTo(modelTransforms);
			modelBoundingSphere = new BoundingSphere();
			foreach (ModelMesh mesh in model.Meshes)
			{
				BoundingSphere meshSphere = mesh.BoundingSphere;
				modelTransforms[mesh.ParentBone.Index].Decompose(out scaling, out rot, out trans);
	            float maxScale = scaling.X;
	            if (maxScale<scaling.Y)
					maxScale = scaling.Y;
	            if (maxScale<scaling.Z)
					maxScale = scaling.Z;

	            float transformedSphereRadius = meshSphere.Radius * maxScale;
				Vector3 transformedSphereCenter = Vector3.Transform(meshSphere.Center, modelTransforms[mesh.ParentBone.Index]);
				BoundingSphere transformedBoundingSphere = new BoundingSphere(transformedSphereCenter, transformedSphereRadius);
				modelBoundingSphere = BoundingSphere.CreateMerged(modelBoundingSphere, meshSphere);
			}
			modelBoundingSphere = modelBoundingSphere.Transform(Matrix.CreateTranslation(new Vector3(transform.position.X, transform.position.Y + modelBoundingSphere.Radius * 2, transform.position.Z)));
        }
        public CollisionComponent(MeshModelComponent comp, TransformComponent transform)
        {
            Model model = comp.model;
            Vector3 trans;
            Vector3 scaling;
            Quaternion rot;
            Random rand = new Random();
            this.boundColor = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
            Matrix[] modelTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelTransforms);
            modelBoundingSphere = new BoundingSphere();
            foreach (ModelMesh mesh in model.Meshes)
            {
                BoundingSphere meshSphere = mesh.BoundingSphere;
                modelTransforms[mesh.ParentBone.Index].Decompose(out scaling, out rot, out trans);
                float maxScale = scaling.X;
                if (maxScale < scaling.Y)
                    maxScale = scaling.Y;
                if (maxScale < scaling.Z)
                    maxScale = scaling.Z;

                float transformedSphereRadius = meshSphere.Radius * maxScale;
                Vector3 transformedSphereCenter = Vector3.Transform(meshSphere.Center, modelTransforms[mesh.ParentBone.Index]);
                BoundingSphere transformedBoundingSphere = new BoundingSphere(transformedSphereCenter, transformedSphereRadius);
                modelBoundingSphere = BoundingSphere.CreateMerged(modelBoundingSphere, meshSphere);
            }
            modelBoundingSphere = modelBoundingSphere.Transform(Matrix.CreateTranslation(new Vector3(transform.position.X, transform.position.Y + modelBoundingSphere.Radius * 2, transform.position.Z)));
        }
    }
}