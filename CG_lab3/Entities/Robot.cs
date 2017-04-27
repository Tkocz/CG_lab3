using CG_lab3.Helpers;
using Manager.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manager.Core;

namespace CG_lab3.Entities
{
    class Robot
    {
        public static Component[] createComponents(String name, bool hasTransformable, Vector3 scale, Vector3 position, Quaternion orientation, Matrix objectWorld)
        {
            MeshModelComponent model = new MeshModelComponent(hasTransformable);
            TransformComponent trans = new TransformComponent(scale, position, orientation, objectWorld);
            return new Component[]
            {
                new CameraComponent(),
                model,
                trans,
                new InputComponent(),
                new CollisionComponent(model, trans)
            };
        }
    }
}