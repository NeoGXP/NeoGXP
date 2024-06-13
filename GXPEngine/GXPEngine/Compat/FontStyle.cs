using SkiaSharp;
using System;

namespace GXPEngine;

/// <summary>
/// Specifies style information applied to text.
/// </summary>
[Flags]
public enum FontStyle
{
	/// <summary>
	/// Normal text.
	/// </summary>
	Regular = 0,
	/// <summary>
	/// Bold text.
	/// </summary>
	Bold = 1,
	/// <summary>
	/// Italic text.
	/// </summary>
	Italic = 2,
}

static internal class FontStyleMethods
{
	public static SKFontStyle ToSkiaFontStyle(this FontStyle style)
	{
		if (style.HasFlag(FontStyle.Bold) && style.HasFlag(FontStyle.Italic))
			return SKFontStyle.BoldItalic;
		if (style.HasFlag(FontStyle.Bold))
			return SKFontStyle.Bold;
		if (style.HasFlag(FontStyle.Italic))
			return SKFontStyle.Italic;
		else
			return SKFontStyle.Normal;
	}
}
