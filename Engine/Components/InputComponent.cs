using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Manager.Core;

namespace Manager.Components
{
    /// <summary>
    /// Component-values, should be made with get-set instead, but, time...
    /// Should also have a constructor for choosing input, if multiplayer or AI
    /// </summary>
    public class InputComponent : Component
    {
        public Keys a, d, w, s, space, lShift, left, right, up, down, q, e, r;

        public InputComponent()
        {
            // --------
            // Translate
            // --------
            a = Keys.A;                 
            d = Keys.D;                  
            w = Keys.W;                     
            s = Keys.S;                 
            space = Keys.Space;         
            lShift = Keys.LeftShift;    

            // ------
            // Rotate
            // ------
            left = Keys.Left;                          
            right = Keys.Right;                        
            up = Keys.Up;                              
            down = Keys.Down;                          
            e = Keys.E;                                
            q = Keys.Q;                 
            r = Keys.R;                 
        }
    }
}
