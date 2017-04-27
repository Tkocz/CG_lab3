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
        public float speed = 0f;
        public Vector3 scale;
        public Matrix Scale;
        public Vector3 position;
        public Quaternion orientation;
        public Matrix objectWorld;
        public float MAXROTATION = MathHelper.PiOver4;
        public float rotationSpeed = 0.003f, modelRotation = 0;
        public Quaternion Rotation;
        public bool direction = true;
        public Keys currentKey;

        public TransformComponent(Vector3 scale, Vector3 position, Quaternion orientation, Matrix objectWorld)
        {
            this.scale = scale;
            Scale = Matrix.CreateScale(scale);
            this.position = position;
            this.orientation = orientation;
            this.objectWorld = objectWorld;
            Rotation = Quaternion.CreateFromAxisAngle(Vector3.Right, MathHelper.PiOver2) * Quaternion.CreateFromAxisAngle(Vector3.Up, MathHelper.Pi);
        }
    }
}
