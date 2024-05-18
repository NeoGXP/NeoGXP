using System;
using GXPEngine.Core;
using Silk.NET.OpenGL.Legacy;

namespace GXPEngine
{
	/// <summary>
	/// Defines different BlendModes. Only six present now, but you can add your own.
	/// </summary>
	public class BlendMode
	{
		/// <summary>
		/// The traditional and default way of blending.
		/// (newColor = spriteColor * spriteAlpha + oldColor * (1-spriteAlpha))
		/// </summary>
		public static readonly BlendMode NORMAL = new BlendMode (
			"Normal", () => {	GLContext.GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);	}
		);

		/// <summary>
		/// The correct way of doing blending, which however requires preparing your sprites for this (non default).
		/// (newColor = spriteColor * 1 + oldColor * (1-spriteAlpha))
		/// </summary>
		public static readonly BlendMode PREMULTIPLIED = new BlendMode(
			"Premultiplied", () => { GLContext.GL.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha); }
		);

		/// <summary>
		/// Multiplying colors - use this for darkening.
		/// (newColor = spriteColor * oldColor + oldColor * 0)
		/// </summary>
		public static readonly BlendMode MULTIPLY = new BlendMode (
			"Multiply", () => {	GLContext.GL.BlendFunc(BlendingFactor.DstColor, BlendingFactor.Zero);	}
		);

		/// <summary>
		/// Brightening existing colors - this mode can be used for lighting effects.
		/// (newColor = spriteColor * oldColor + oldColor * 1)
		/// </summary>
		public static readonly BlendMode LIGHTING = new BlendMode(
			"Lighting", () => { GLContext.GL.BlendFunc(BlendingFactor.DstColor, BlendingFactor.One); }
		);

		/// <summary>
		/// Adding colors - use this e.g. for "volumetric" lighting effects.
		/// (newColor = spriteColor * 1 + oldColor * 1)
		/// </summary>
		public static readonly BlendMode ADDITIVE = new BlendMode(
			"Additive", () => { GLContext.GL.BlendFunc(BlendingFactor.One, BlendingFactor.One); }
		);

		/// <summary>
		/// This mode can be used  to fill in empty screen parts (e.g. drawing a background after adding lights to the foreground).
		/// (newColor = spriteColor * (1-oldColorAlpha) + oldColor * oldColorAlpha)
		/// </summary>
		public static readonly BlendMode FILLEMPTY = new BlendMode(
			"Fill", () => { GLContext.GL.BlendFunc(BlendingFactor.OneMinusDstAlpha, BlendingFactor.DstAlpha); }
		);

		public delegate void Action();

		/// <summary>
		/// This should point to an anonymous function updating the blendfunc
		/// </summary>
		public readonly Action enable;

		/// <summary>
		/// A label for this blendmode
		/// </summary>
		public readonly string label;

		public BlendMode (string pLabel, Action pEnable)
		{
			if (pEnable == null) {
				throw new Exception ("Enabled action cannot be null");
			} else {
				enable = pEnable;
			}

			label = pLabel;
		}

		public override string ToString ()
		{
			return label;
		}

	}
}

