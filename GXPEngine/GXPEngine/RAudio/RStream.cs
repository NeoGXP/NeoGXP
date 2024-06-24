using Raylib_cs;

namespace GXPEngine.Core;

public class RStream : IRAudio
{
	private readonly Music _music;
	private readonly uint _baseSampleRate;

	public RStream(string filename, bool looping)
	{
		_music = Raylib.LoadMusicStream(filename);
		_music.Looping = looping;
		_baseSampleRate = _music.Stream.SampleRate;
		Frequency = _baseSampleRate;
		Pan = 0.5f; //center
	}

	~RStream()
	{
		Raylib.UnloadMusicStream(_music);
	}

	private float _volume;
	private float _pitch;
	private float _pan;
	private bool _paused;

	public float Volume
	{
		get => _volume;
		set
		{
			_volume = Mathf.Clamp(Mathf.Abs(value), 0f, 1f);
			Raylib.SetMusicVolume(_music, _volume);
		}
	}

	public float Frequency
	{
		get => _pitch * _baseSampleRate;
		set
		{
			_pitch = value / _baseSampleRate;
			Raylib.SetMusicPitch(_music, _pitch);
		}
	}

	public float Pan
	{
		get => _pan;
		set
		{
			_pan = Mathf.Clamp(value, 0f, 1f);
			Raylib.SetMusicPan(_music, _pan);
		}
	}

	public bool Paused
	{
		get => _paused;
		set
		{
			_paused = value;
			if (_paused)
			{
				Raylib.PauseMusicStream(_music);
			}
			else
			{
				Raylib.ResumeMusicStream(_music);
			}
		}
	}

	public bool IsPlaying => Raylib.IsMusicStreamPlaying(_music);

	public void Play()
	{
		Raylib.PlayMusicStream(_music);
	}

	public void Stop()
	{
		Raylib.StopMusicStream(_music);
	}

	public void Update()
	{
		Raylib.UpdateMusicStream(_music);
	}
}
