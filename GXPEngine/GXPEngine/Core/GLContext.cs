//#define USE_FMOD_AUDIO
#define STRETCH_ON_RESIZE

using System;
using Silk.NET.GLFW;
using Silk.NET.OpenGL.Legacy;

namespace GXPEngine.Core {

	class WindowSize {
		public static WindowSize instance = new WindowSize();
		public int width, height;
	}

	public class GLContext {

		const int MAXKEYS = 348;
		const int MAXBUTTONS = 10;

		private static bool[] keys = new bool[MAXKEYS+1];
		private static bool[] keydown = new bool[MAXKEYS+1];
		private static bool[] keyup = new bool[MAXKEYS+1];
		private static bool[] buttons = new bool[MAXBUTTONS+1];
		private static bool[] mousehits = new bool[MAXBUTTONS+1];
		private static bool[] mouseup = new bool[MAXBUTTONS+1]; //mouseup kindly donated by LeonB
		private static int keyPressedCount = 0;
		private static bool anyKeyDown = false;

		public static int mouseX = 0;
		public static int mouseY = 0;

		private Game _owner;
        private static SoundSystem _soundSystem;

		private int _targetFrameRate = 60;
		private long _lastFrameTime = 0;
		private long _lastFPSTime = 0;
		private int _frameCount = 0;
		private int _lastFPS = 0;
		private bool _vsyncEnabled = false;

		private static double _realToLogicWidthRatio;
		private static double _realToLogicHeightRatio;

		public static Glfw GLFW;
		public static GL GL;
		private unsafe WindowHandle* _window;


		//------------------------------------------------------------------------------------------------------------------------
		//														RenderWindow()
		//------------------------------------------------------------------------------------------------------------------------
		public GLContext (Game owner) {
			_owner = owner;
			_lastFPS = _targetFrameRate;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Width
		//------------------------------------------------------------------------------------------------------------------------
		public int width {
			get { return WindowSize.instance.width; }
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Height
		//------------------------------------------------------------------------------------------------------------------------
		public int height {
			get { return WindowSize.instance.height; }
		}

        //------------------------------------------------------------------------------------------------------------------------
        //														SoundSystem
        //------------------------------------------------------------------------------------------------------------------------
        public static SoundSystem soundSystem
        {
            get
            {
				if (_soundSystem == null) {
					InitializeSoundSystem ();
				}
                return _soundSystem;
            }
        }

		//------------------------------------------------------------------------------------------------------------------------
		//														setupWindow()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void CreateWindow(int width, int height, bool fullScreen, bool vSync, int realWidth, int realHeight) {
			// This stores the "logical" width, used by all the game logic:
			WindowSize.instance.width = width;
			WindowSize.instance.height = height;
			_realToLogicWidthRatio = (double)realWidth / width;
			_realToLogicHeightRatio = (double)realHeight / height;
			_vsyncEnabled = vSync;

			GLFW = Glfw.GetApi();

			bool success = GLFW.Init();
			if (success == false) {
				throw new Exception("Failed to initialize GLFW");
			}

			GLFW.WindowHint(WindowHintInt.Samples, 8);

			_window = GLFW.CreateWindow(realWidth, realHeight, "Game", fullScreen ? GLFW.GetPrimaryMonitor() : null, null);
			if (_window == null)
			{
				GLFW.Terminate();
				throw new Exception("Failed to create GLFW window");
			}
			GLFW.MakeContextCurrent(_window);
			GLFW.SwapInterval(vSync ? 1 : 0);

			GL = GL.GetApi(GLFW.GetProcAddress);
			if (GL == null)
			{
				GLFW.Terminate();
				throw new Exception("Failed to initialise GL");
			}


			GLFW.SetKeyCallback(_window, (handle, key, code, action, mods) =>
			{
				if (key == Keys.Unknown) return;
				int keyValue = (int)key;
				bool press = action == InputAction.Press;
				if (press) { keydown[keyValue] = true; anyKeyDown = true; keyPressedCount++; }
				else { keyup[keyValue] = true; keyPressedCount--; }
				keys[keyValue] = press;
			});


			GLFW.SetMouseButtonCallback(_window, (handle, button, action, mods) =>
			{
				int buttonValue = (int)button;
				bool press = action == InputAction.Press;
				if (press) mousehits[buttonValue] = true;
				else mouseup[buttonValue] = true;
				buttons[buttonValue] = press;
			});

			GLFW.SetFramebufferSizeCallback(_window, FramebufferSizeCallback);
			void FramebufferSizeCallback(WindowHandle* handle, int newWidth, int newHeight)
			{
				GL.Viewport(0, 0, (uint)newWidth, (uint)newHeight);
				GL.Enable(EnableCap.Multisample);
				GL.Enable(EnableCap.Texture2D);
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
				GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Fastest);
				// GL.Enable (EnableCap.PolygonSmooth);
				GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

				// Load the basic projection settings:
				GL.MatrixMode(MatrixMode.Projection);
				GL.LoadIdentity();

#if STRETCH_ON_RESIZE
				_realToLogicWidthRatio = (double)newWidth / WindowSize.instance.width;
				_realToLogicHeightRatio = (double)newHeight / WindowSize.instance.height;
#endif
				// Here's where the conversion from logical width/height to real width/height happens:
				GL.Ortho(0.0f, newWidth / _realToLogicWidthRatio, newHeight / _realToLogicHeightRatio, 0.0f, 0.0f, 1000.0f);
#if !STRETCH_ON_RESIZE
				lock (WindowSize.instance) {
					WindowSize.instance.width = (int)(newWidth/_realToLogicWidthRatio);
					WindowSize.instance.height = (int)(newHeight/_realToLogicHeightRatio);
				}
#endif

				if (Game.main != null)
				{
					Game.main.RenderRange = new Rectangle(0, 0, WindowSize.instance.width, WindowSize.instance.height);
				}
			}
			//Apparently this isn't called by default anymore, so we need to call it manually
			FramebufferSizeCallback(_window, width, height);

			InitializeSoundSystem ();
		}

		private static void InitializeSoundSystem() {
#if USE_FMOD_AUDIO
			_soundSystem = new FMODSoundSystem();
#else
			_soundSystem = new SoloudSoundSystem();
#endif
			_soundSystem.Init();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														ShowCursor()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void ShowCursor (bool enable)
		{
			GLFW.SetInputMode(_window, CursorStateAttribute.Cursor, enable ? CursorModeValue.CursorNormal : CursorModeValue.CursorHidden);
		}

		public void SetVSync(bool enableVSync) {
			_vsyncEnabled = enableVSync;
			GLFW.SwapInterval(_vsyncEnabled ? 1 : 0);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														SetScissor()
		//------------------------------------------------------------------------------------------------------------------------
		public void SetScissor(int x, int y, int width, int height) {
			if ((width == WindowSize.instance.width) && (height == WindowSize.instance.height)) {
				GL.Disable(EnableCap.ScissorTest);
			} else {
				GL.Enable(EnableCap.ScissorTest);
			}

			GL.Scissor(
				(int)(x*_realToLogicWidthRatio),
				(int)(y*_realToLogicHeightRatio),
				(uint)(width*_realToLogicWidthRatio),
				(uint)(height*_realToLogicHeightRatio)
			);
			//GL.Scissor(x, y, width, height);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Close()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void Close() {
			_soundSystem.Deinit();
			GLFW.SetWindowShouldClose(_window, true);
			GLFW.Terminate();
			System.Environment.Exit(0);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Run()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void Run() {
			GLFW.SetTime(0.0);
			do {
				if (_vsyncEnabled || (Time.time - _lastFrameTime > (1000 / _targetFrameRate))) {
					_lastFrameTime = Time.time;

					//actual fps count tracker
					_frameCount++;
					if (Time.time - _lastFPSTime > 1000) {
						_lastFPS = (int)(_frameCount / ((Time.time -_lastFPSTime) / 1000.0f));
						_lastFPSTime = Time.time;
						_frameCount = 0;
					}

					UpdateMouseInput();
					_owner.Step();
                    _soundSystem.Step();

					ResetHitCounters();
					Display();

					Time.newFrame ();
					GLFW.PollEvents();
				}


			} while (!GLFW.WindowShouldClose(_window));
		}


		//------------------------------------------------------------------------------------------------------------------------
		//														display()
		//------------------------------------------------------------------------------------------------------------------------
		private unsafe void Display () {
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();

			_owner.Render(this);

			GLFW.SwapBuffers(_window);
			if (GetKey(Key.ESCAPE)) this.Close();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														SetColor()
		//------------------------------------------------------------------------------------------------------------------------
		public void SetColor (byte r, byte g, byte b, byte a) {
			GL.Color4(r, g, b, a);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														PushMatrix()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void PushMatrix(float[] matrix) {
			GL.PushMatrix ();
			fixed (float* ptr = matrix)
			{
				GL.MultMatrix(ptr);
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														PopMatrix()
		//------------------------------------------------------------------------------------------------------------------------
		public void PopMatrix() {
			GL.PopMatrix ();
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														DrawQuad()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void DrawQuad(float[] vertices, float[] uv) {
			GL.EnableClientState( EnableCap.TextureCoordArray );
			GL.EnableClientState( EnableCap.VertexArray );
			fixed (float* ptr = uv)
			{
				GL.TexCoordPointer( 2, TexCoordPointerType.Float, 0, ptr);
			}
			fixed (float* ptr = vertices)
			{
				GL.VertexPointer( 2, VertexPointerType.Float, 0, ptr);
			}
			GL.DrawArrays(PrimitiveType.Quads, 0, 4);
			GL.DisableClientState(EnableCap.VertexArray);
			GL.DisableClientState(EnableCap.TextureCoordArray);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetKey()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetKey(int key) {
			return keys[key];
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetKeyDown()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetKeyDown(int key) {
			return keydown[key];
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetKeyUp()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetKeyUp(int key) {
			return keyup[key];
		}

		public static bool AnyKey() {
			return keyPressedCount > 0;
		}

		public static bool AnyKeyDown() {
			return anyKeyDown;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetMouseButton()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetMouseButton(int button) {
			return buttons[button];
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetMouseButtonDown()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetMouseButtonDown(int button) {
			return mousehits[button];
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														GetMouseButtonUp()
		//------------------------------------------------------------------------------------------------------------------------
		public static bool GetMouseButtonUp(int button) {
			return mouseup[button];
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														ResetHitCounters()
		//------------------------------------------------------------------------------------------------------------------------
		public static void ResetHitCounters() {
			Array.Clear (keydown, 0, MAXKEYS);
			Array.Clear (keyup, 0, MAXKEYS);
			Array.Clear (mousehits, 0, MAXBUTTONS);
			Array.Clear (mouseup, 0, MAXBUTTONS);
			anyKeyDown = false;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														UpdateMouseInput()
		//------------------------------------------------------------------------------------------------------------------------
		public unsafe void UpdateMouseInput() {
			GLFW.GetCursorPos(_window, out double x, out double y);
			mouseX = (int)(x / _realToLogicWidthRatio);
			mouseY = (int)(y / _realToLogicHeightRatio);
		}

		public int currentFps {
			get { return _lastFPS; }
		}

		public int targetFps {
			get { return _targetFrameRate; }
			set {
				if (value < 1) {
					_targetFrameRate = 1;
				} else {
					_targetFrameRate = value;
				}
			}
		}

	}

}
