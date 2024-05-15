using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	private readonly Sprite _sprite;	// Create a sprite from an image file

	public MyGame() : base(800, 600, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		_sprite = new Sprite("colors.png");
		_sprite.SetXY(100, 100);
		this.AddChild(_sprite);
	}

	void Update()
	{
		_sprite.rotation += 0.1f;
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}
