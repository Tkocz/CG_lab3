using CG_lab3.Entities;
using Manager;
using Manager.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG_lab3.Helpers
{
    public class StaticObjects
    {
        private List<String> names = new List<string>() { "SmallBoy", "FireRock", "BigBoySmooth" };
        private String name;
        private Vector3 scale, position, speed;
        private Quaternion orientation;
        private Matrix objectWorld;
        public StaticObjects(int nObjects)
        {
            Random rnd = new Random();
            for(int i=0; i<nObjects; i++)
            {

                name = names[rnd.Next(3)];
                scale = new Vector3(0.05f, 0.05f, 0.05f);
                position= new Vector3((float)(rnd.NextDouble() * 1081), 180f, (float) -(rnd.NextDouble() * 1081));
                
                position.Y = getHeightMapY(position);
                orientation = Quaternion.Identity;
                objectWorld = Matrix.Identity;
                speed = Vector3.Zero;

                Engine.GetInst().addEntity(WorldObject.createComponents(
                    name,
                    scale,
                    position,
                    orientation,
                    objectWorld
                    ));
            }
        }
        public float getHeightMapY(Vector3 position)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var hMComp = entity.GetComponent<HeightmapComponent>();
                if (hMComp != null)
                {
                    int xvalue = (int)position.X;
                    int zvalue = (int)position.Z;
                    var yValue = hMComp.heightMapData[xvalue, -zvalue];
                    return yValue;
                }
            }
            return 0f;
        }
    }
}
