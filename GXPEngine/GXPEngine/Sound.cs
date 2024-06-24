using System;
using System.Threading;
using System.Collections.Generic;
using GXPEngine.Core;
using System.Diagnostics.CodeAnalysis;

namespace GXPEngine
{
	/// <summary>
	/// The Sound Class represents a Sound resource in memory
	/// You can load .mp3, .ogg or .wav
	/// </summary>
	public class Sound
	{
		[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Local")]
		private record Entry(string Filename, bool Looping);
		private static Dictionary<Entry, FileRSound> _soundCache = new Dictionary<Entry, FileRSound>();

		private IFileRAudio _id;
		private SoundSystem _system;

		/// <summary>
		/// Creates a new <see cref="GXPEngine.Sound"/>.
		/// This class represents a sound file.
		/// Sound files are loaded into memory unless you set them to 'streamed'.
		/// An optional parameter allows you to create a looping sound.
		/// </summary>
		/// <param name='filename'>
		/// Filename should include path and extension.
		/// </param>
		/// <param name='looping'>
		/// If set to <c>true</c> the sound file repeats itself. (It loops)
		/// </param>
		/// <param name='streaming'>
		/// If set to <c>true</c>, the file will be streamed rather than loaded into memory.
		/// </param>
		/// <param name='cached'>
		/// If set to <c>true</c>, the sound will be stored in cache, preserving memory when creating the same sound multiple times.
		/// </param>
		public Sound(string filename, bool looping = false, bool streaming = false)
		{
			_system = GLContext.soundSystem;

			if (streaming) {
				_id = _system.CreateStream(filename, looping);
			} else
			{
				Entry entry = new(filename, looping);
				if (_soundCache.TryGetValue(entry, out FileRSound value))
				{
					_id = value;
				}
				else
				{
					FileRSound soundFile = _system.LoadSound(filename, looping);
					_id = soundFile;
					_soundCache[entry] = soundFile;
				}
			}
		}

		/// <summary>
		/// Play the specified sound and return the newly created SoundChannel
		/// </summary>
		/// <param name='paused'>
		/// When set to <c>true</c>, the sound is set up, but remains paused.
		/// You can use this to set frequency, panning and volume before playing the sound.
		/// </param>
		public SoundChannel Play(bool paused = false, float volume=1, float pan=0)
		{
			IRAudio audio = _id.GetPlayableCopy();
			SoundChannel soundChannel = _system.PlaySound(audio, paused, volume, pan);
			return soundChannel;
		}
	}
}
