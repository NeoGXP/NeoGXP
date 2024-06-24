using System;

namespace GXPEngine.Core;

public class FileRStream : IFileRAudio
{
	private readonly string _filename;
	private readonly bool _looping;

	public FileRStream(string filename, bool looping)
	{
		_filename = filename;
		_looping = looping;
	}

	public IRAudio GetPlayableCopy()
	{
		return new RStream(_filename, _looping);
	}
}
