﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EngineName.Components.Renderable
{
    public abstract class C3DRenderable:CRenderable
    {
        public Model model;
        public Texture2D texture;
        public Texture2D normalMap;
		public Effect effect;
		public TextureCube environmentMap;
    }
}
