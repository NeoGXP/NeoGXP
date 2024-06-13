using SkiaSharp;
using System;
using System.Collections;

using static GXPEngine.Core.GLContext;
using GLEnum = Silk.NET.OpenGL.Legacy.GLEnum;
using InternalFormat = Silk.NET.OpenGL.Legacy.InternalFormat;
using PixelFormat = Silk.NET.OpenGL.Legacy.PixelFormat;
using PixelType = Silk.NET.OpenGL.Legacy.PixelType;
using TextureParameterName = Silk.NET.OpenGL.Legacy.TextureParameterName;
using TextureTarget = Silk.NET.OpenGL.Legacy.TextureTarget;


namespace GXPEngine.Core
{
	public class Texture2D
	{
		private static Hashtable LoadCache = new Hashtable();
		private static Texture2D lastBound = null;
		
		const int UNDEFINED_GLTEXTURE 	= 0;
		
		private SKBitmap _bitmap;
		private uint _glTexture;
		private string _filename = "";
		private int count = 0;
		private bool stayInCache = false;

		//------------------------------------------------------------------------------------------------------------------------
		//														Texture2D()
		//------------------------------------------------------------------------------------------------------------------------
		public Texture2D (int width, int height) {
			if (width == 0) if (height == 0) return;
			SetBitmap (new SKBitmap(width, height));
		}
		public Texture2D (string filename) {
			Load (filename);
		}
		public Texture2D (SKBitmap bitmap) {
			SetBitmap (bitmap);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetInstance()
		//------------------------------------------------------------------------------------------------------------------------
		public static Texture2D GetInstance (string filename, bool keepInCache=false) {
			Texture2D tex2d = LoadCache[filename] as Texture2D;
			if (tex2d == null) {
				tex2d = new Texture2D(filename);
				LoadCache[filename] = tex2d;
			}
			tex2d.stayInCache |= keepInCache; // setting it once to true keeps it in cache
			tex2d.count ++;
			return tex2d;
		}


		//------------------------------------------------------------------------------------------------------------------------
		//														RemoveInstance()
		//------------------------------------------------------------------------------------------------------------------------
		public static void RemoveInstance (string filename)
		{
			if (LoadCache.ContainsKey (filename)) {
				Texture2D tex2D = LoadCache[filename] as Texture2D;
				tex2D.count --;
				if (tex2D.count == 0 && !tex2D.stayInCache) LoadCache.Remove (filename);
			}
		}

		public void Dispose () {
			if (_filename != "") {
				Texture2D.RemoveInstance (_filename);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														bitmap
		//------------------------------------------------------------------------------------------------------------------------
		public SKBitmap bitmap {
			get { return _bitmap; }
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														filename
		//------------------------------------------------------------------------------------------------------------------------
		public string filename {
			get { return _filename; }
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														width
		//------------------------------------------------------------------------------------------------------------------------
		public int width {
			get { return _bitmap.Width; }
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														height
		//------------------------------------------------------------------------------------------------------------------------
		public int height {
			get { return _bitmap.Height; }
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Bind()
		//------------------------------------------------------------------------------------------------------------------------
		public void Bind() {
			if (lastBound == this) return;
			lastBound = this;
			GL.BindTexture(TextureTarget.Texture2D, _glTexture);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Unbind()
		//------------------------------------------------------------------------------------------------------------------------
		public void Unbind() {
			//GL.BindTexture (GL.TEXTURE_2D, 0);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Load()
		//------------------------------------------------------------------------------------------------------------------------
		private void Load(string filename) {
			_filename = filename;
			SKBitmap bitmap;
			try {
				bitmap = SKBitmap.Decode(filename);
			} catch {
				throw new Exception("Image " + filename + " cannot be found.");
			}
			SetBitmap(bitmap);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														SetBitmap()
		//------------------------------------------------------------------------------------------------------------------------
		private void SetBitmap(SKBitmap bitmap) {
			_bitmap = bitmap;
			CreateGLTexture ();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														CreateGLTexture()
		//------------------------------------------------------------------------------------------------------------------------
		private void CreateGLTexture ()
		{
			if (_glTexture != UNDEFINED_GLTEXTURE)
				destroyGLTexture ();

			if (_bitmap == null)
				_bitmap = new SKBitmap (64, 64);

			GL.GenTextures (1, out _glTexture);

			GL.BindTexture (TextureTarget.Texture2D, _glTexture);
			if (Game.main.PixelArt) {
				GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (uint)GLEnum.Nearest);
				GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (uint)GLEnum.Nearest);
			} else {
				GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (uint)GLEnum.Linear);
				GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (uint)GLEnum.Linear);
			}
			GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (uint)GLEnum.ClampToEdge);
			GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (uint)GLEnum.ClampToEdge);

			UpdateGLTexture();
			GL.BindTexture (TextureTarget.Texture2D, 0);
			lastBound = null;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														UpdateGLTexture()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void UpdateGLTexture() {
			GL.BindTexture(TextureTarget.Texture2D, _glTexture);
			GL.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)_bitmap.Width, (uint)_bitmap.Height, 0,
				PixelFormat.Bgra, PixelType.UnsignedByte, bitmap.GetPixels().ToPointer());

			lastBound = null;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														destroyGLTexture()
		//------------------------------------------------------------------------------------------------------------------------
		private void destroyGLTexture() {
			GL.DeleteTextures(1, _glTexture);
			_glTexture = UNDEFINED_GLTEXTURE;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Clone()
		//------------------------------------------------------------------------------------------------------------------------
		public Texture2D Clone (bool deepCopy=false) {
			SKBitmap bitmap;
			if (deepCopy) {
				bitmap = _bitmap.Copy();
			} else {
				bitmap = _bitmap;
			}
			Texture2D newTexture = new Texture2D(0, 0);
			newTexture.SetBitmap(bitmap);
			return newTexture;
		}

		public bool wrap {
			set {
				GL.BindTexture (TextureTarget.Texture2D, _glTexture);
				GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (uint)(value?GLEnum.Repeat:GLEnum.ClampToEdge));
				GL.TexParameterI (TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (uint)(value?GLEnum.Repeat:GLEnum.ClampToEdge));
				GL.BindTexture (TextureTarget.Texture2D, 0);
				lastBound = null;
			}
		}

		public static string GetDiagnostics() {
			string output = "";
			output += "Number of textures in cache: " + LoadCache.Keys.Count+'\n';
			return output;
		}
	}
}

