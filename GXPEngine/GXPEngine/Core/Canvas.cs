using System;
using GXPEngine.Core;
using SkiaSharp;

namespace GXPEngine
{
	/// <summary>
	/// The Canvas object can be used for drawing 2D visuals at runtime.
	/// </summary>
	public class Canvas : Sprite
	{
		protected SKCanvas _graphics;
		protected bool _invalidate = false;

		//------------------------------------------------------------------------------------------------------------------------
		//														Canvas()
		//------------------------------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the Canvas class.
		/// It is a regular GameObject that can be added to any displaylist via commands such as AddChild.
		/// It contains a <a href="https://learn.microsoft.com/en-us/dotnet/api/skiasharp.skcanvas">SkiaSharp.Canvas</a> component.
		/// </summary>
		/// <param name='width'>
		/// Width of the canvas in pixels.
		/// </param>
		/// <param name='height'>
		/// Height of the canvas in pixels.
		/// </param>
		public Canvas (int width, int height, bool addCollider=true) : this(new SKBitmap (width, height), addCollider)
		{
			name = width + "x" + height;
		}

		public Canvas (SKBitmap bitmap, bool addCollider=true) : base (bitmap,addCollider)
		{
			_graphics = new SKCanvas(bitmap);
			_invalidate = true;
		}

		public Canvas(string filename, bool addCollider=true):base(filename, false, addCollider)
		{
			_graphics = new SKCanvas(texture.bitmap);
			_invalidate = true;
		}


		//------------------------------------------------------------------------------------------------------------------------
		//														graphics
		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Returns the graphics component. This interface provides tools to draw on the sprite.
		/// See: <a href="https://learn.microsoft.com/en-us/dotnet/api/skiasharp.skcanvas">SkiaSharp.Canvas</a>
		/// </summary>
		public SKCanvas graphics {
			get { 
				_invalidate = true;
				return _graphics; 
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Render()
		//------------------------------------------------------------------------------------------------------------------------
		override protected void RenderSelf(GLContext glContext) {
			if (_invalidate) {
				_texture.UpdateGLTexture ();
				_invalidate = false;
			}

			base.RenderSelf (glContext);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														alpha
		//------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Draws a Sprite onto this Canvas.
		/// It will ignore color and alpha, but it will use position, rotation, scale, origin, current frame.
		/// </summary>
		/// <param name='sprite'>
		/// The Sprite that should be drawn.
		/// </param>
		public void DrawSprite(Sprite sprite) {
			SKMatrix44 mat = SKMatrix44.FromRowMajor(sprite.matrix);

			//For the cropping of the AnimationSprite, which works by changing the sampling UVs:
			float wd = sprite.texture.width;
			float ht = sprite.texture.height;
			float[] uvs = sprite.GetUVs();
			SKRect sourceRect = SKRect.Create(uvs[0]*wd,uvs[1]*ht,(uvs[2]-uvs[0])*wd,(uvs[5]-uvs[1])*ht);
			SKRect destRect = SKRect.Create(sprite.width, sprite.height);

			graphics.Save();
			graphics.SetMatrix(in mat);
			using (SKImage image = SKImage.FromBitmap(sprite.texture.bitmap))
			{
				graphics.DrawImage(image, sourceRect, destRect, new SKSamplingOptions(SKFilterMode.Linear));
			}
			graphics.Restore();
		}

		// Called by the garbage collector
		~Canvas() {
			_graphics.Dispose();
		}
	}
}

