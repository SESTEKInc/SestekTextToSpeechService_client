namespace TextToSpeech.RestServiceContracts.DataContracts
{
	public class TtsResponse
	{

		public  byte[] Audio { get; set; }
		public bool Success { get; set; }
		public string ErrorMessage { get; set; }
		public string ErrorCode { get; set; }
		public string MoreInfo { get; set; }
		public  string Warnings { get; set; }
	
	}
}
