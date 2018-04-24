using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHTools.RHLib.RH
{
	[Flags]
	public enum NoteFlags
	{
		DownLeft = 1,
		Left = 2,
		UpLeft = 4,
		Down = 8,
		Center = 16,
		Up = 32,
		UpRight = 64,
		Right = 128,
		DownRight = 256
	}
}
