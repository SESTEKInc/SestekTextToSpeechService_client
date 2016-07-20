using System.Collections.Generic;

namespace TextToSpeech.RestServiceContracts.DataContracts
{
	public class TtsRequest
	{
		public TtsRequest()
		{
			Voice = new VoiceInformation();
			Audio = new AudioInfo();
			License = new Dictionary<string, string>();
		}

		public  string Text { get; set; }
		public VoiceInformation Voice { get; set; }
		public AudioInfo Audio { get; set; }
		public Dictionary<string, string> License { get; set; }

	}
}
