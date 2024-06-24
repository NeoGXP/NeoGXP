using Raylib_cs;
using System;
using System.Collections.Generic;

namespace GXPEngine.Core;

public class RAudioSoundSystem : SoundSystem
{
	private readonly LinkedList<WeakReference<IRAudio>> _musicStreams = [];

	public override void Init()
	{
		Raylib.InitAudioDevice();
	}

	public override void Deinit()
	{
		Raylib.CloseAudioDevice();
	}

	public override FileRStream CreateStream(string filename, bool looping)
	{
		return new FileRStream(filename, looping);
	}

	public override FileRSound LoadSound(string filename, bool looping)
	{
		return new FileRSound(filename, looping);
	}

	public override void Step()
	{
		var node = _musicStreams.First;
		while(node != null)
		{
			var next = node.Next;
			if (node.ValueRef.TryGetTarget(out IRAudio audio))
			{
				audio.Update();
			}
			else
			{
				_musicStreams.Remove(node);
			}
			node = next;
		}
	}

	public override SoundChannel PlaySound(IRAudio audio, bool paused)
	{
		_musicStreams.AddLast(new WeakReference<IRAudio>(audio));
		SoundChannel soundChannel = new(audio)
		{
			IsPaused = paused,
		};
		if (!paused) soundChannel.Play();
		return soundChannel;
	}

	public override SoundChannel PlaySound(IRAudio audio, bool paused, float volume, float pan)
	{
		_musicStreams.AddLast(new WeakReference<IRAudio>(audio));
		SoundChannel soundChannel = new(audio)
		{
			IsPaused = paused,
			Volume = volume,
			Pan = pan,
		};
		if (!paused) soundChannel.Play();
		return soundChannel;
	}

	public override float GetChannelFrequency(IRAudio audio)
	{
		return audio.Frequency;
	}

	public override void SetChannelFrequency(IRAudio audio, float frequency)
	{
		audio.Frequency = frequency;
	}

	public override float GetChannelPan(IRAudio audio)
	{
		return (1f - audio.Pan) * 2f - 1f;
	}

	public override void SetChannelPan(IRAudio audio, float pan)
	{
		audio.Pan = pan / -2f + 0.5f;
	}

	public override float GetChannelVolume(IRAudio audio)
	{
		return audio.Volume;
	}

	public override void SetChannelVolume(IRAudio audio, float volume)
	{
		audio.Volume = volume;
	}

	public override bool GetChannelPaused(IRAudio audio)
	{
		return audio.Paused;
	}

	public override void SetChannelPaused(IRAudio audio, bool pause)
	{
		audio.Paused = pause;
	}

	public override bool ChannelIsPlaying(IRAudio audio)
	{
		return audio.IsPlaying;
	}

	public override void StopChannel(IRAudio audio)
	{
		audio.Stop();
	}
}
