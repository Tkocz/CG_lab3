using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Manager.Helpers
{
	public class Utils
	{
		public static void DrawBoundingBox(BoundingBox bBox, Color color, GraphicsDevice device, BasicEffect basicEffect, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
		{
			Vector3 v1 = bBox.Min;
			Vector3 v2 = bBox.Max;

			VertexPositionColor[] cubeLineVertices = new VertexPositionColor[8];
			cubeLineVertices[0] = new VertexPositionColor(v1, color);
			cubeLineVertices[1] = new VertexPositionColor(new Vector3(v2.X, v1.Y, v1.Z), color);
			cubeLineVertices[2] = new VertexPositionColor(new Vector3(v2.X, v1.Y, v2.Z), color);
			cubeLineVertices[3] = new VertexPositionColor(new Vector3(v1.X, v1.Y, v2.Z), color);

			cubeLineVertices[4] = new VertexPositionColor(new Vector3(v1.X, v2.Y, v1.Z), color);
			cubeLineVertices[5] = new VertexPositionColor(new Vector3(v2.X, v2.Y, v1.Z), color);
			cubeLineVertices[6] = new VertexPositionColor(v2, color);
			cubeLineVertices[7] = new VertexPositionColor(new Vector3(v1.X, v2.Y, v2.Z), color);

			short[] cubeLineIndices = { 0, 1, 1, 2, 2, 3, 3, 0, 4, 5, 5, 6, 6, 7, 7, 4, 0, 4, 1, 5, 2, 6, 3, 7 };
			RasterizerState rs = new RasterizerState();
			basicEffect.World = worldMatrix;
			basicEffect.View = viewMatrix;
			basicEffect.Projection = projectionMatrix;
			basicEffect.VertexColorEnabled = true;
			rs.FillMode = FillMode.Solid;
			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.DrawUserIndexedPrimitives(PrimitiveType.LineList, cubeLineVertices, 0, 8, cubeLineIndices, 0, 12);

			}
		}
		public static void DrawSphere(BoundingSphere sphere, Color color, GraphicsDevice device, BasicEffect basicEffect, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix)
		{
			Vector3 up = sphere.Center + sphere.Radius * Vector3.Up;
			Vector3 down = sphere.Center + sphere.Radius * Vector3.Down;
			Vector3 right = sphere.Center + sphere.Radius * Vector3.Right;
			Vector3 left = sphere.Center + sphere.Radius * Vector3.Left;
			Vector3 forward = sphere.Center + sphere.Radius * Vector3.Forward;
			Vector3 back = sphere.Center + sphere.Radius * Vector3.Backward;

			VertexPositionColor[] sphereLineVertices = new VertexPositionColor[6];
			sphereLineVertices[0] = new VertexPositionColor(up, color);
			sphereLineVertices[1] = new VertexPositionColor(down, color);
			sphereLineVertices[2] = new VertexPositionColor(left, color);
			sphereLineVertices[3] = new VertexPositionColor(right, color);
			sphereLineVertices[4] = new VertexPositionColor(forward, color);
			sphereLineVertices[5] = new VertexPositionColor(back, color);

			basicEffect.World = worldMatrix;
			basicEffect.View = viewMatrix;
			basicEffect.Projection = projectionMatrix;
			basicEffect.VertexColorEnabled = true;
			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.DrawUserPrimitives(PrimitiveType.LineList, sphereLineVertices, 0, 3);


			}
		}
	}
}
