using System;
using System.Text;
using TextToSpeech.RestClient;
using TextToSpeech.SmokeTests.TestSuite;

namespace TextToSpeech.SmokeTests.TestDefinitions.AvaliableVoices
{
	public class AvaliableVoicesSmokeTest : ISmokeTest
	{
		private readonly TextToSpeechServiceRestClient restClient;

		public AvaliableVoicesSmokeTest(TextToSpeechServiceRestClient 
			textToSpeechServiceRestClient)
		{
			Name = "AvaliableVoicesSmokeTest";
			Description = "Will Try To Get Avaliable Voices From TTS Service";

			restClient = textToSpeechServiceRestClient;
		}

        public string Description { get; set; }
        public string Name { get; set; }

		public SmokeTestResult ExceuteTest()
		{
			try
			{
				return ExceuteAvaliableVoicesTest();
			}
			catch (Exception exception)
			{
				return FailedResult(exception.Message, exception.ToString());
			}
		}

		private SmokeTestResult ExceuteAvaliableVoicesTest()
		{
			var testResult = new SmokeTestResult();
			var avaliableVoices = restClient.GetAvaliableVoices();

			testResult.Success = avaliableVoices.Success;
			testResult.ErrorMessage = avaliableVoices.ErrorMessage;
			testResult.ErrorCode = avaliableVoices.ErrorCode;
			testResult.MoreInfo = avaliableVoices.MoreInfo;

			var resultInfoBuilder = new StringBuilder();
			resultInfoBuilder.AppendLine("");
			resultInfoBuilder.AppendLine(".......................");

			resultInfoBuilder.AppendLine("Voice Count :" + avaliableVoices.Count);

			if (avaliableVoices.Success)
			{
				foreach (var voice in avaliableVoices.Voices)
				{
					var voiceInfo = " Voice Name : " + voice.Name +
					                " Language   : " + voice.Language +
					                " Gender     : " + voice.Gender;

					resultInfoBuilder.AppendLine(voiceInfo);
				}
			}


			resultInfoBuilder.AppendLine(".......................");
			testResult.ResultInfo = resultInfoBuilder.ToString();


			return testResult;
		}

		private SmokeTestResult FailedResult(string errorMessage, string moreInfo)
		{
			var testResult = new SmokeTestResult
			{
				Success = false,
				ErrorMessage = errorMessage,
				MoreInfo = moreInfo
			};
			return testResult;
		}
	}
}