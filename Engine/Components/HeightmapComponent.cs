using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;

namespace Manager.Components
{
    /// <summary>
    /// Component-values, should be made with get-set instead, but, time...
    /// </summary>
    public class HeightmapComponent : Component
    {
        //Holds all the data related to the height map(e.g.height data, vertex/index buffers).
        public Texture2D heightMap;
		public Texture2D heightMapTexture;
		public VertexPositionNormalTexture[] vertices;
		public VertexBuffer vertexBuffer;
		public IndexBuffer indexBuffer;
		public int terrainWidth;
		public int terrainHeight;
		public int nHeightMapChunks;
		public BasicEffect basicEffect;
		public int[] indices;
		public float[,] heightMapData;

		public struct HeightMapChunk
		{
			public BoundingBox chunkBoundingBox;
			public VertexPositionNormalTexture[] vertices;
			public int[] indices;
			public VertexBuffer vertexBuffer;
			public IndexBuffer indexBuffer;
			public Color boundColor;
			public float heighestPoint;
		}

		public HeightMapChunk [] heightMapChunk;

		public HeightmapComponent(string heighMap, string heightMapTexture, int nHeightMapChunks, int prefHeightMapWidth = 0, int prefHeightMapHeight = 0)
		{
			this.nHeightMapChunks = nHeightMapChunks * nHeightMapChunks;
			heightMap = Engine.GetInst().Content.Load<Texture2D>(heighMap);
			this.heightMapTexture = Engine.GetInst().Content.Load<Texture2D>(heightMapTexture);
			if (prefHeightMapWidth == 0)
				terrainWidth = heightMap.Width;
			else
				terrainWidth = prefHeightMapWidth;
			if (prefHeightMapHeight == 0)
				terrainHeight = heightMap.Height;
			else
				terrainHeight = prefHeightMapHeight;
			basicEffect = new BasicEffect(Engine.GetInst().GraphicsDevice);
			heightMapChunk = new HeightMapChunk[this.nHeightMapChunks];
		}
	}
}