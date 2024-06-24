namespace GXPEngine.Core;

public interface IRAudio
{
	float Volume { get; set; }
	float Frequency { get; set; }
	float Pan { get; set; }
	bool Paused { get; set; }
	bool IsPlaying { get; }
	public void Play();
	public void Stop();
	public void Update();
}
