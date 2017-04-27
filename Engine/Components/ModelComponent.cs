using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Manager.Core;

namespace Manager.Components
{
    /// <summary>
    /// Component-values, should be made with get-set instead, but, time...
    /// </summary>
    public class ModelComponent : Component
    {
        //Holds a model and the data transforms for its meshes
        public Model model;
        public bool hasTransformable;
        public BasicEffect modelEffect;
        public ModelComponent(string modelName, bool hasTransformable)
        {
            model = Engine.GetInst().Content.Load<Model>(modelName);
            this.hasTransformable = hasTransformable;
        }
    }
}
