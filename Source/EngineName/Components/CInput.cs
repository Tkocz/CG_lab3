using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineName.Core;
using Microsoft.Xna.Framework.Input;

namespace EngineName.Components
{
	public class CInput : EcsComponent
	{
		//public Keys ForwardMovementKey = Keys.W;
		//public Keys BackwardMovementKey = Keys.S;
		//public Keys LeftMovementKey = Keys.A;
		//public Keys RightMovementKey = Keys.D;
		public Keys CameraMovementForward = Keys.Up;
		public Keys CameraMovementBackward = Keys.Down;
		public Keys CameraMovementRight = Keys.Right;
		public Keys CameraMovementLeft = Keys.Left;
		public Keys CameraMovementUp = Keys.Space;
		public Keys CameraMovementDown = Keys.LeftShift;
		public Keys CameraTiltUp = Keys.W;
		public Keys CameraTiltDown = Keys.S;
		//public Keys ZRotationPlus = Keys.Z;
		//
	}
}
