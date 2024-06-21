using System;
using System.Threading;
using System.Collections.Generic;
using GXPEngine.Core;

namespace GXPEngine
{
	/// <summary>
	/// The Sound Class represents a Sound resource in memory
	/// You can load .mp3, .ogg or .wav
	/// </summary>
	public class Sound
	{
        private static Dictionary<string, IntPtr> _soundCache = new Dictionary<string, IntPtr>();

        private IntPtr _id;
        private SoundSystem _system;

        /// <summary>
        /// Creates a new <see cref="GXPEngine.Sound"/>.
        /// This class represents a sound file.
        /// Sound files are loaded into memory unless you set them to 'streamed'.
        /// An optional parameter allows you to create a looping sound.
        /// </summary>
        /// <param name='filename'>
        /// Filename, should include path and extension.
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
			} else {
                if (_soundCache.TryGetValue(filename, out IntPtr value))
                {
                     _id = value;
                }
                else
                {
                    _id = _system.LoadSound(filename, looping);
                    if (_id == IntPtr.Zero)
                    {
                        throw new Exception("Sound file not found: " + filename);
                    }
                    _soundCache[filename] = _id;
                }
            }
		}

		~Sound()
		{
		}

		/// <summary>
		/// Play the specified paused and return the newly created SoundChannel
		/// </summary>
		/// <param name='paused'>
		/// When set to <c>true</c>, the sound is set up, but remains paused.
		/// You can use this to set frequency, panning and volume before playing the sound.
		/// </param>
		public SoundChannel Play(bool paused = false, float volume=1, float pan=0)
		{
			uint channelID = _system.PlaySound(_id, paused, volume, pan);
			SoundChannel soundChannel = new SoundChannel(channelID);
			return soundChannel;
		}
	}
}
