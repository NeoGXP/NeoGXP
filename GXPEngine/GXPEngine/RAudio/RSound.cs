using Raylib_cs;
using System;

namespace GXPEngine.Core;

public class RSound : IRAudio
{
	private readonly Raylib_cs.Sound _sound;
	private readonly bool _looping;
	private readonly uint _baseSampleRate;

	private readonly float _totalSamples;
	private float _progressSeconds = 0;

	public RSound(Raylib_cs.Sound sound, bool looping)
	{
		_sound = Raylib.LoadSoundAlias(sound);
		_looping = looping;
		_baseSampleRate = sound.Stream.SampleRate;
		_totalSamples = sound.FrameCount;
		Frequency = _baseSampleRate;
		Pan = 0.5f; //center
	}

	~RSound()
	{
		Raylib.UnloadSoundAlias(_sound);
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
			_volume = value;
			Raylib.SetSoundVolume(_sound, _volume);
		}
	}

	public float Frequency
	{
		get => _pitch * _baseSampleRate;
		set
		{
			_pitch = value / _baseSampleRate;
			Raylib.SetSoundPitch(_sound, _pitch);
		}
	}

	public float Pan
	{
		get => _pan;
		set
		{
			_pan = Mathf.Clamp(value, 0f, 1f);
			Raylib.SetSoundPan(_sound, _pan);
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
				Raylib.PauseSound(_sound);
			}
			else
			{
				Raylib.ResumeSound(_sound);
			}
		}
	}

	public bool IsPlaying => Raylib.IsSoundPlaying(_sound);

	public void Play()
	{
		Raylib.PlaySound(_sound);
	}

	public void Stop()
	{
		Raylib.StopSound(_sound);
	}

	public void Update()
	{
		if (_looping && !Paused)
		{
			float samplesPerSecond = Frequency;
			float samplesPassedSinceLastUpdate = samplesPerSecond * (Time.deltaTime / 1000f);
			_progressSeconds += samplesPassedSinceLastUpdate;
			if (_progressSeconds >= _totalSamples)
			{
				Raylib.PlaySound(_sound); //start from start
				_progressSeconds = 0;
			}
		}
	}
}
