using EngineName.Components;
using EngineName.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineName.Systems
{
    public class TransformSystem : EcsSystem
    {
        public override void Update(float t, float dt)
        {
            foreach (CTransform transformComponent in Game1.Inst.Scene.GetComponents<CTransform>().Values)
            {
                Vector3 scale = transformComponent.Scale;
				Quaternion orientation = transformComponent.Orientation;
                Matrix objectWorld = transformComponent.ObjectWorld;
                Vector3 position = transformComponent.Position;
				Quaternion addRot = Quaternion.CreateFromYawPitchRoll(transformComponent.YPR.X, transformComponent.YPR.Y, transformComponent.YPR.Z);
				addRot.Normalize();
                orientation *= addRot;
                transformComponent.ObjectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);
				transformComponent.Orientation = orientation;
			}
        }
    }
}
