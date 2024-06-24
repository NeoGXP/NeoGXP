using System;
using System.Collections.Generic;

namespace GXPEngine.Core
{
    public abstract class SoundSystem
    {
        public abstract void Init();
        public abstract void Deinit();
        public abstract FileRStream CreateStream(string filename, bool looping);
        public abstract FileRSound LoadSound(string filename, bool looping);
        public abstract void Step();
        public abstract SoundChannel PlaySound(IRAudio audio, bool paused);
		public abstract SoundChannel PlaySound(IRAudio audio, bool paused, float volume, float pan);

        public abstract float GetChannelFrequency(IRAudio audio);
        public abstract void SetChannelFrequency(IRAudio audio, float frequency);
        public abstract float GetChannelPan(IRAudio audio);
        public abstract void SetChannelPan(IRAudio audio, float pan);
        public abstract float GetChannelVolume(IRAudio audio);
        public abstract void SetChannelVolume(IRAudio audio, float volume);
        public abstract bool GetChannelPaused(IRAudio audio);
        public abstract void SetChannelPaused(IRAudio audio, bool pause);
        public abstract bool ChannelIsPlaying(IRAudio audio);
        public abstract void StopChannel(IRAudio audio);
    }
}
