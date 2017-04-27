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
                var scale = transformComponent.Scale;
                var orientation = transformComponent.Orientation;
                var objectWorld = transformComponent.ObjectWorld;
                var position = transformComponent.Position;
                transformComponent.ObjectWorld = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(orientation) * Matrix.CreateTranslation(position);
            }
        }
    }
}
