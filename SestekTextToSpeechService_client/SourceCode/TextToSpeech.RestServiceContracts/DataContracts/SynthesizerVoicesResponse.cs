using System.Collections.Generic;

namespace TextToSpeech.RestServiceContracts.DataContracts
{
	public class SynthesizerVoicesResponse
	{

		public SynthesizerVoicesResponse()
		{
			Voices = new List<SynthesizerVoice>();
			Success = true;
		}

		public List<SynthesizerVoice> Voices { get; set; }

		public int Count
		{
			get { return Voices.Count; }
		}

		public  bool Success { get; set; }
        public  string ErrorMessage { get; set; }
		public  string ErrorCode { get; set; }
		public string MoreInfo { get; set; }

		
	}
}
