using System;									// System contains a lot of default C# libraries
using GXPEngine;								// GXPEngine contains the engine
using GXPEngine.Core;							// GXPEngine.Core contains Vector2

public class MyGame : Game
{
	private readonly Sprite _sprite;	// Create a sprite from an image file
	private readonly EasyDraw _easyDraw;	// Create a canvas to draw on
	private readonly AnimationSprite _barry;	// Create an animation sprite
	private readonly Sprite _circle;	// Create a sprite to draw a circle
	private readonly Sound _ping;	// Create a sound
	private readonly Sound _pingLooped; // Create a looped sound
	private readonly SoundChannel _bgm;	// Create a background music

	private Vector2 _dir;
	private int _frameCount = 0;

	public MyGame() : base(800, 600, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		_sprite = new Sprite("assets/colors.png");
		_sprite.SetXY(100, 100);
		this.AddChild(_sprite);

		_easyDraw = new EasyDraw(400, 300, false);
		_easyDraw.Clear(Color.Chartreuse);
		_easyDraw.Fill(255, 0, 0);

		//Default font
		Console.WriteLine("Font name: " + _easyDraw.font.Typeface.FamilyName + "\n Size: " + _easyDraw.font.Size);
		_easyDraw.TextAlign(CenterMode.Min, CenterMode.Min);
		_easyDraw.Text("Hello, World!", 200, 0);
		_easyDraw.Ellipse(200, 0, 3, 3);

		//Loaded from file (normal)
		_easyDraw.TextFont(Utils.LoadFont("assets/Roboto/Roboto-Regular.ttf", 15));
		Console.WriteLine("Font name: " + _easyDraw.font.Typeface.FamilyName + "\n Size: " + _easyDraw.font.Size);
		_easyDraw.TextAlign(CenterMode.Center, CenterMode.Center);
		_easyDraw.Text("Hello, World!", 260, 50);
		_easyDraw.Ellipse(260, 50, 3, 3);

		//Loaded from file (bold)
		_easyDraw.TextFont(Utils.LoadFont("assets/Roboto/Roboto-Bold.ttf", 15));
		Console.WriteLine("Font name: " + _easyDraw.font.Typeface.FamilyName + "\n Size: " + _easyDraw.font.Size);
		_easyDraw.TextAlign(CenterMode.Max, CenterMode.Max);
		_easyDraw.Text("Hello, World!", 325, 100);
		_easyDraw.Ellipse(325, 100, 3, 3);

		//System font
		_easyDraw.TextFont("Comic Sans MS", 15, FontStyle.Bold | FontStyle.Italic);
		Console.WriteLine("Font name: " + _easyDraw.font.Typeface.FamilyName + "\n Size: " + _easyDraw.font.Size);
		_easyDraw.TextAlign(CenterMode.Center, CenterMode.Center);
		_easyDraw.Text("Hello, World!", 260, 120);
		_easyDraw.Ellipse(260, 120, 3, 3);

		_easyDraw.SetXY(width/2f-10, height/2f-10);
		this.AddChild(_easyDraw);

		_barry = new AnimationSprite("assets/barry.png", 7, 1);
		_barry.SetCycle(0, 3, 100, true);
		_barry.SetXY(400, 100);
		this.AddChild(_barry);

		_circle = new Sprite("assets/circle.png");
		_circle.SetOrigin(_circle.width/2f, _circle.height/2f);
		this.AddChild(_circle);

		_ping = new Sound("assets/ping.wav", looping:false);
		_pingLooped = new Sound("assets/ping.wav", looping:true);

		Sound bgm = new Sound("assets/file.ogg", streaming:true, looping:true);
		// Sound bgm = new Sound("assets/file.wav", streaming:true, looping:true);
		_bgm = bgm.Play();
		Console.WriteLine(_bgm.Frequency);
	}

	void Update()
	{
		Gizmos.SetColor(255, 255, 255);
		Gizmos.DrawArrow(10, 10, 60, 60);

		// Sprite
		_sprite.rotation += 0.1f;
		Gizmos.SetColor(255, 0, 0);
		Gizmos.DrawArrowAngle(100, 100, _sprite.rotation, 60);

		// EasyDraw
		if (Input.GetMouseButton(1)) _easyDraw.Clear(Color.Chartreuse);
		_easyDraw.Stroke(0, 0, 255);
		_easyDraw.StrokeWeight(2);
		_easyDraw.Rect(250, 250, 200, 20);
		_easyDraw.DrawSprite(_sprite);

		// Barry
		_barry.Animate(Time.deltaTime);

		// Circle
		_circle.SetXY(Input.mouseX, Input.mouseY);

		// Ping
		if (Input.GetMouseButtonDown(0))
		{
			_ping.Play();
		}
		if (Input.GetMouseButtonDown(1))
		{
			SoundChannel play = _pingLooped.Play();
			play.Frequency *= 2;
		}

		// Dir
		_dir = new Vector2(
			Input.GetKey(Key.RIGHT) ? 1 : Input.GetKey(Key.LEFT) ? -1 : 0.01f,
			Input.GetKey(Key.DOWN) ? 1 : Input.GetKey(Key.UP) ? -1 : 0.01f
			);

		Gizmos.SetColor(0, 255, 0);
		Gizmos.DrawArrow(150, 400, _dir.x * 50, _dir.y * 50);

		// BGM
		_bgm.Pan = (Input.mouseX - width / 2f) / (width / 2f);
		_bgm.Volume = Input.mouseY / -(float)height;
		if (Input.GetKeyDown(Key.SPACE))
		{
			_bgm.IsPaused = !_bgm.IsPaused;
		}
		_bgm.Frequency = 44100 + Input.mouseX * 100; //pain
		if (Input.GetKeyDown(Key.M))
		{
			_bgm.Mute = !_bgm.Mute;
		}

		if (_frameCount == 1)
		{
			SaveFrame("test.png");
		}
		_frameCount++;
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}
