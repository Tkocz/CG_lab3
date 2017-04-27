using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Helpers
{
    class CubeMesh
    {
        public VertexPositionColor[] vertices;
        public VertexBuffer vertexBuffer;

        public short[] indices;
        public IndexBuffer indexBuffer;

        // Vertex positions
        private static readonly Vector3 FRONT_TOP_LEFT = new Vector3(-0.5f, 1f, 0.5f);
        private static readonly Vector3 FRONT_TOP_RIGHT = new Vector3(0.5f, 1f, 0.5f);
        private static readonly Vector3 FRONT_BOTTOM_LEFT = new Vector3(-0.5f, 0f, 0.5f);
        private static readonly Vector3 FRONT_BOTTOM_RIGHT = new Vector3(0.5f, 0f, 0.5f);
        private static readonly Vector3 BACK_TOP_LEFT = new Vector3(-0.5f, 1f, -0.5f);
        private static readonly Vector3 BACK_TOP_RIGHT = new Vector3(0.5f, 1f, -0.5f);
        private static readonly Vector3 BACK_BOTTOM_LEFT = new Vector3(-0.5f, 0f, -0.5f);
        private static readonly Vector3 BACK_BOTTOM_RIGHT = new Vector3(0.5f, 0f, -0.5f);

        public CubeMesh()
        {
            SetupVertices();
            SetupVertexBuffer();

            SetupIndices();
            SetupIndexBuffer();
        }
        private void SetupVertices()
        {
            List<VertexPositionColor> vertexList = new List<VertexPositionColor>(36);

            // Front face
            vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT, Color.Red));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Red));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT, Color.Red));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT, Color.Red));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT, Color.Red));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Red));

            // Top face
            vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT, Color.Green));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT, Color.Green));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT, Color.Green));
            vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT, Color.Green));
            vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT, Color.Green));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT, Color.Green));

            // Right face
            vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT, Color.Blue));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT, Color.Blue));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Blue));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_RIGHT, Color.Blue));
            vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT, Color.Blue));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT, Color.Blue));

            // Bottom face
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT, Color.Yellow));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT, Color.Yellow));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT, Color.Yellow));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT, Color.Yellow));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_RIGHT, Color.Yellow));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT, Color.Yellow));

            // Left face
            vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT, Color.White));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT, Color.White));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT, Color.White));
            vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT, Color.White));
            vertexList.Add(new VertexPositionColor(FRONT_TOP_LEFT, Color.White));
            vertexList.Add(new VertexPositionColor(FRONT_BOTTOM_LEFT, Color.White));

            // Back face
            vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT, Color.Black));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT, Color.Black));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_RIGHT, Color.Black));
            vertexList.Add(new VertexPositionColor(BACK_TOP_RIGHT, Color.Black));
            vertexList.Add(new VertexPositionColor(BACK_TOP_LEFT, Color.Black));
            vertexList.Add(new VertexPositionColor(BACK_BOTTOM_LEFT, Color.Black));

            vertices = vertexList.ToArray();
        }

        private void SetupVertexBuffer()
        {
            vertexBuffer = new VertexBuffer(Engine.GetInst().GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.None);
            vertexBuffer.SetData(vertices);
        }

        private void SetupIndices()
        {
            List<short> indexList = new List<short>(36);

            for (short i = 0; i < 36; ++i)
                indexList.Add(i);

            indices = indexList.ToArray();
        }

        private void SetupIndexBuffer()
        {
            indexBuffer = new IndexBuffer(Engine.GetInst().GraphicsDevice, typeof(short), indices.Length, BufferUsage.None);
            indexBuffer.SetData(indices);
        }
    }
}