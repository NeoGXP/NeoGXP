using System;
using GXPEngine.Core;

namespace GXPEngine
{
	/// <summary>
	/// This class represents a sound channel on the soundcard.
	/// </summary>
	public class SoundChannel
	{
		private IRAudio _rAudio;
        private SoundSystem _system;
        private float _volume = 1f;
        private bool _isMuted = false;

        public SoundChannel(IRAudio rAudio)
		{
            _system = GLContext.soundSystem;
            _rAudio = rAudio;
        }

        /// <summary>
        /// Gets or sets the channel frequency.
        /// </summary>
        /// <value>
        /// The frequency. Defaults to the sound frequency. (Usually 44100Hz)
        /// </value>
        public float Frequency 
		{
			get 
			{
                float frequency = _system.GetChannelFrequency(_rAudio);
				return frequency;
			}
			set
			{
                _system.SetChannelFrequency(_rAudio, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="GXPEngine.SoundChannel"/> is mute.
		/// </summary>
		/// <value>
		/// <c>true</c> if you want to mute the sound
		/// </value>
		public bool Mute   
		{
			get 
			{
				return _isMuted;
			}
			set
			{
                _isMuted = value;
                if (value)
                {
                    _system.SetChannelVolume(_rAudio, 0f);
                }
                else
                {
                    _system.SetChannelVolume(_rAudio, _volume);
                }
            }
		}

		/// <summary>
		/// Gets or sets the pan. Value should be in range -1..0..1, for left..center..right
		/// </summary>
		public float Pan   
		{
			get 
			{
                return _system.GetChannelPan(_rAudio);
			}
			set
			{
                _system.SetChannelPan(_rAudio, value);
			}
		}		

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="GXPEngine.Channel"/> is paused.
		/// </summary>
		/// <value>
		/// <c>true</c> if paused; otherwise, <c>false</c>.
		/// </value>
		public bool IsPaused   
		{
			get 
			{
                return _system.GetChannelPaused(_rAudio);
			}
			set
			{
                _system.SetChannelPaused(_rAudio, value);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="GXPEngine.Channel"/> is playing. (readonly)
		/// </summary>
		/// <value>
		/// <c>true</c> if playing; otherwise, <c>false</c>.
		/// </value>
		public bool IsPlaying  
		{
			get 
			{
                return _system.ChannelIsPlaying(_rAudio);
			}
		}

		/// <summary>
		/// (Re)start the channel.
		/// </summary>
		public void Play()
		{
			_rAudio.Play();
		}
		
		/// <summary>
		/// Stop the channel.
		/// </summary>
		public void Stop()
		{
            _system.StopChannel(_rAudio);
		}
	
		/// <summary>
		/// Gets or sets the volume. Should be in range 0...1
		/// </summary>
		/// <value>
		/// The volume.
		/// </value>
		public float Volume 
		{
			get 
			{
                return _system.GetChannelVolume(_rAudio);
			}
			set
			{
                _volume = value;
                if (!_isMuted)
                {
                    _system.SetChannelVolume(_rAudio, value);
                }
			}
		}
		
	}
}
