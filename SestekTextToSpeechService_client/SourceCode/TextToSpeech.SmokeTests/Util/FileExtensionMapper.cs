using System;
using TextToSpeech.RestServiceContracts.DataContracts;

namespace TextToSpeech.SmokeTests.Util
{
	public static class FileExtensionMapper
	{
		public static string Map(AudioFormat format)
		{
			switch (format)
			{
				case AudioFormat.Wav:
					return ".wav";
				case AudioFormat.Opus:
					return ".opus";
				case AudioFormat.Mp3:
					return ".mp3";
				case AudioFormat.Flv:
					return ".m4a";
			}

			throw new ArgumentException("Invalid Audio Format " + format);
		}
	}
}