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
    public class TransformComponent : Component
    {
        //Holds data such as position, rotation and scaling
        public Vector3 speed = new Vector3(0.1f, 0.01f, 0.01f);
        public Vector3 scale;
        public Vector3 position;
        public Quaternion orientation;
        public Matrix objectWorld;

        public TransformComponent(Vector3 scale, Vector3 position, Quaternion orientation, Matrix objectWorld)
        {
            this.scale = scale;
            this.position = position;
            this.orientation = orientation;
            this.objectWorld = objectWorld;
        }
    }
}
