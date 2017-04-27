using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manager.Core;

namespace CG_lab3.Entities
{
    /// <summary>
    /// A predefinition of the components needed for the main entity, merely a convenient shortcut
    /// </summary>
    public class Tropper
    {
        public static Component[] createComponents(String name, bool hasTransformables, Vector3 scale, Vector3 position, Quaternion orientation, Matrix objectWorld, Vector3 speed)
        {
			ModelComponent model = new ModelComponent(name, hasTransformables);
			TransformComponent trans = new TransformComponent(scale, position, orientation, objectWorld);
			return new Component[]
			{
				model,
				trans,
				new CollisionComponent(model, trans)
            };
        }
    }
}