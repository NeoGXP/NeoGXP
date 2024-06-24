using Raylib_cs;
using System;

namespace GXPEngine.Core;

public class FileRSound : IFileRAudio
{
	private readonly Raylib_cs.Sound _sound;
	private readonly bool _looping;

	public FileRSound(string filename, bool looping)
	{
		_sound = Raylib.LoadSound(filename);
		_looping = looping;
	}

	~FileRSound()
	{
		Raylib.UnloadSound(_sound);
	}

	public IRAudio GetPlayableCopy()
	{
		return new RSound(_sound, _looping);
	}
}
