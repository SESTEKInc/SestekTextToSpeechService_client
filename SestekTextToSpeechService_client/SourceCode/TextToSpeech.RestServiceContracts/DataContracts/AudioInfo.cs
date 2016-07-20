using System.Collections.Generic;

namespace TextToSpeech.RestServiceContracts.DataContracts
{
	public class AudioInfo
	{
		public AudioInfo()
		{
			FormatDetails = new Dictionary<string, string>();
		}

		public AudioFormat Format { get; set; }

		public Dictionary<string, string> FormatDetails { get; set; }
	}
}
