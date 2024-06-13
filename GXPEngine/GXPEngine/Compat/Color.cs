using SkiaSharp;

namespace GXPEngine;

/// <summary>
/// Compatibility class for translating the old System.Drawing colors to the new SKColors.
/// </summary>
public static class Color
{
	public static SKColor Transparent => SKColors.Transparent;
	public static SKColor AliceBlue => SKColors.AliceBlue;
	public static SKColor AntiqueWhite => SKColors.AntiqueWhite;
	public static SKColor Aqua => SKColors.Aqua;
	public static SKColor Aquamarine => SKColors.Aquamarine;
	public static SKColor Azure => SKColors.Azure;
	public static SKColor Beige => SKColors.Beige;
	public static SKColor Bisque => SKColors.Bisque;
	public static SKColor Black => SKColors.Black;
	public static SKColor BlanchedAlmond => SKColors.BlanchedAlmond;
	public static SKColor Blue => SKColors.Blue;
	public static SKColor BlueViolet => SKColors.BlueViolet;
	public static SKColor Brown => SKColors.Brown;
	public static SKColor BurlyWood => SKColors.BurlyWood;
	public static SKColor CadetBlue => SKColors.CadetBlue;
	public static SKColor Chartreuse => SKColors.Chartreuse;
	public static SKColor Chocolate => SKColors.Chocolate;
	public static SKColor Coral => SKColors.Coral;
	public static SKColor CornflowerBlue => SKColors.CornflowerBlue;
	public static SKColor Cornsilk => SKColors.Cornsilk;
	public static SKColor Crimson => SKColors.Crimson;
	public static SKColor Cyan => SKColors.Cyan;
	public static SKColor DarkBlue => SKColors.DarkBlue;
	public static SKColor DarkCyan => SKColors.DarkCyan;
	public static SKColor DarkGoldenrod => SKColors.DarkGoldenrod;
	public static SKColor DarkGray => SKColors.DarkGray;
	public static SKColor DarkGreen => SKColors.DarkGreen;
	public static SKColor DarkKhaki => SKColors.DarkKhaki;
	public static SKColor DarkMagenta => SKColors.DarkMagenta;
	public static SKColor DarkOliveGreen => SKColors.DarkOliveGreen;
	public static SKColor DarkOrange => SKColors.DarkOrange;
	public static SKColor DarkOrchid => SKColors.DarkOrchid;
	public static SKColor DarkRed => SKColors.DarkRed;
	public static SKColor DarkSalmon => SKColors.DarkSalmon;
	public static SKColor DarkSeaGreen => SKColors.DarkSeaGreen;
	public static SKColor DarkSlateBlue => SKColors.DarkSlateBlue;
	public static SKColor DarkSlateGray => SKColors.DarkSlateGray;
	public static SKColor DarkTurquoise => SKColors.DarkTurquoise;
	public static SKColor DarkViolet => SKColors.DarkViolet;
	public static SKColor DeepPink => SKColors.DeepPink;
	public static SKColor DeepSkyBlue => SKColors.DeepSkyBlue;
	public static SKColor DimGray => SKColors.DimGray;
	public static SKColor DodgerBlue => SKColors.DodgerBlue;
	public static SKColor Firebrick => SKColors.Firebrick;
	public static SKColor FloralWhite => SKColors.FloralWhite;
	public static SKColor ForestGreen => SKColors.ForestGreen;
	public static SKColor Fuchsia => SKColors.Fuchsia;
	public static SKColor Gainsboro => SKColors.Gainsboro;
	public static SKColor GhostWhite => SKColors.GhostWhite;
	public static SKColor Gold => SKColors.Gold;
	public static SKColor Goldenrod => SKColors.Goldenrod;
	public static SKColor Gray => SKColors.Gray;
	public static SKColor Green => SKColors.Green;
	public static SKColor GreenYellow => SKColors.GreenYellow;
	public static SKColor Honeydew => SKColors.Honeydew;
	public static SKColor HotPink => SKColors.HotPink;
	public static SKColor IndianRed => SKColors.IndianRed;
	public static SKColor Indigo => SKColors.Indigo;
	public static SKColor Ivory => SKColors.Ivory;
	public static SKColor Khaki => SKColors.Khaki;
	public static SKColor Lavender => SKColors.Lavender;
	public static SKColor LavenderBlush => SKColors.LavenderBlush;
	public static SKColor LawnGreen => SKColors.LawnGreen;
	public static SKColor LemonChiffon => SKColors.LemonChiffon;
	public static SKColor LightBlue => SKColors.LightBlue;
	public static SKColor LightCoral => SKColors.LightCoral;
	public static SKColor LightCyan => SKColors.LightCyan;
	public static SKColor LightGoldenrodYellow => SKColors.LightGoldenrodYellow;
	public static SKColor LightGreen => SKColors.LightGreen;
	public static SKColor LightGray => SKColors.LightGray;
	public static SKColor LightPink => SKColors.LightPink;
	public static SKColor LightSalmon => SKColors.LightSalmon;
	public static SKColor LightSeaGreen => SKColors.LightSeaGreen;
	public static SKColor LightSkyBlue => SKColors.LightSkyBlue;
	public static SKColor LightSlateGray => SKColors.LightSlateGray;
	public static SKColor LightSteelBlue => SKColors.LightSteelBlue;
	public static SKColor LightYellow => SKColors.LightYellow;
	public static SKColor Lime => SKColors.Lime;
	public static SKColor LimeGreen => SKColors.LimeGreen;
	public static SKColor Linen => SKColors.Linen;
	public static SKColor Magenta => SKColors.Magenta;
	public static SKColor Maroon => SKColors.Maroon;
	public static SKColor MediumAquamarine => SKColors.MediumAquamarine;
	public static SKColor MediumBlue => SKColors.MediumBlue;
	public static SKColor MediumOrchid => SKColors.MediumOrchid;
	public static SKColor MediumPurple => SKColors.MediumPurple;
	public static SKColor MediumSeaGreen => SKColors.MediumSeaGreen;
	public static SKColor MediumSlateBlue => SKColors.MediumSlateBlue;
	public static SKColor MediumSpringGreen => SKColors.MediumSpringGreen;
	public static SKColor MediumTurquoise => SKColors.MediumTurquoise;
	public static SKColor MediumVioletRed => SKColors.MediumVioletRed;
	public static SKColor MidnightBlue => SKColors.MidnightBlue;
	public static SKColor MintCream => SKColors.MintCream;
	public static SKColor MistyRose => SKColors.MistyRose;
	public static SKColor Moccasin => SKColors.Moccasin;
	public static SKColor NavajoWhite => SKColors.NavajoWhite;
	public static SKColor Navy => SKColors.Navy;
	public static SKColor OldLace => SKColors.OldLace;
	public static SKColor Olive => SKColors.Olive;
	public static SKColor OliveDrab => SKColors.OliveDrab;
	public static SKColor Orange => SKColors.Orange;
	public static SKColor OrangeRed => SKColors.OrangeRed;
	public static SKColor Orchid => SKColors.Orchid;
	public static SKColor PaleGoldenrod => SKColors.PaleGoldenrod;
	public static SKColor PaleGreen => SKColors.PaleGreen;
	public static SKColor PaleTurquoise => SKColors.PaleTurquoise;
	public static SKColor PaleVioletRed => SKColors.PaleVioletRed;
	public static SKColor PapayaWhip => SKColors.PapayaWhip;
	public static SKColor PeachPuff => SKColors.PeachPuff;
	public static SKColor Peru => SKColors.Peru;
	public static SKColor Pink => SKColors.Pink;
	public static SKColor Plum => SKColors.Plum;
	public static SKColor PowderBlue => SKColors.PowderBlue;
	public static SKColor Purple => SKColors.Purple;
	public static SKColor RebeccaPurple => new(102, 51, 153);
	public static SKColor Red => SKColors.Red;
	public static SKColor RosyBrown => SKColors.RosyBrown;
	public static SKColor RoyalBlue => SKColors.RoyalBlue;
	public static SKColor SaddleBrown => SKColors.SaddleBrown;
	public static SKColor Salmon => SKColors.Salmon;
	public static SKColor SandyBrown => SKColors.SandyBrown;
	public static SKColor SeaGreen => SKColors.SeaGreen;
	public static SKColor SeaShell => SKColors.SeaShell;
	public static SKColor Sienna => SKColors.Sienna;
	public static SKColor Silver => SKColors.Silver;
	public static SKColor SkyBlue => SKColors.SkyBlue;
	public static SKColor SlateBlue => SKColors.SlateBlue;
	public static SKColor SlateGray => SKColors.SlateGray;
	public static SKColor Snow => SKColors.Snow;
	public static SKColor SpringGreen => SKColors.SpringGreen;
	public static SKColor SteelBlue => SKColors.SteelBlue;
	public static SKColor Tan => SKColors.Tan;
	public static SKColor Teal => SKColors.Teal;
	public static SKColor Thistle => SKColors.Thistle;
	public static SKColor Tomato => SKColors.Tomato;
	public static SKColor Turquoise => SKColors.Turquoise;
	public static SKColor Violet => SKColors.Violet;
	public static SKColor Wheat => SKColors.Wheat;
	public static SKColor White => SKColors.White;
	public static SKColor WhiteSmoke => SKColors.WhiteSmoke;
	public static SKColor Yellow => SKColors.Yellow;
	public static SKColor YellowGreen => SKColors.YellowGreen;

	//------------------------------------------------------------------------------------------------------------------------
	//														SKColor from ARGB
	//------------------------------------------------------------------------------------------------------------------------
	public static SKColor FromArgb(int a, int r, int g, int b)
	{
		return new SKColor((byte)r, (byte)g, (byte)b, (byte)a);
	}

	public static SKColor FromArgb(int a, SKColor color)
	{
		return new SKColor(color.Red, color.Green, color.Blue, (byte)a);
	}
}
