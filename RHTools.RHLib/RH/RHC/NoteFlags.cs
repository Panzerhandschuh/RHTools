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
		Pad1DownLeft = 1,
		Pad1Left = 2,
		Pad1UpLeft = 4,
		Pad1Down = 8,
		Pad1Center = 16,
		Pad1Up = 32,
		Pad1UpRight = 64,
		Pad1Right = 128,
		Pad1DownRight = 256,
		Pad2DownLeft = 512,
		Pad2Left = 1024,
		Pad2UpLeft = 2048,
		Pad2Down = 4096,
		Pad2Center = 8192,
		Pad2Up = 16384,
		Pad2UpRight = 32768,
		Pad2Right = 65536,
		Pad2DownRight = 131072
	}
}
