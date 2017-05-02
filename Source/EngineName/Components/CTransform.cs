using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineName.Core;
using Microsoft.Xna.Framework;

namespace EngineName.Components
{
    public class CTransform:EcsComponent
    {
        public Vector3 Speed = new Vector3(0.1f, 0.01f, 0.01f);
        public Vector3 Scale;
        public Vector3 Position;
		public Vector3 YPR;
        public Quaternion Orientation;
        public Matrix ObjectWorld;
        public CTransform() { }

        public CTransform(Vector3 scale, Vector3 position, Quaternion orientation, Matrix objectWorld)
        {
            Scale = scale;
            Position = position;
            Orientation = orientation;
            ObjectWorld = objectWorld;
        }
    }
}
