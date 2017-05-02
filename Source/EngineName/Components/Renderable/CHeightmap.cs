using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EngineName.Components.Renderable
{
    public class CHeightmap : C3DRenderable {
        public Texture2D Image;
        public Color[,] HeightData;
        internal Color[,] ColorMap;
        public float HeighestPoint;
        public float LowestPoint;

        public CHeightmap()
        {
            texture = Game1.Inst.Content.Load<Texture2D>("Textures/mudcrack");
        }
    }
}
