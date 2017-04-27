using Manager.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Helpers
{
    public class HeightMapHelper
    {
        public float getHeightMapY(Vector3 position)
        {
            foreach (var entity in Engine.GetInst().Entities.Values)
            {
                var hMComp = entity.GetComponent<HeightmapComponent>();
                if (hMComp != null)
                {
                    int xvalue = (int)position.X;
                    int zvalue = (int)position.Z;
                    try
                    {
                        var yValue = hMComp.heightMapData[xvalue, -zvalue];
                        return yValue;
                    }
                    catch (Exception ex)
                    {
                        return 200f;
                    }
                }
            }
            return 0f;
        }
    }
}