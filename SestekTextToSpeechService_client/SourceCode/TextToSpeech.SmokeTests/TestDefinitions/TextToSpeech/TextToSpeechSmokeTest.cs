using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextToSpeech.RestClient;
using TextToSpeech.RestServiceContracts.DataContracts;
using TextToSpeech.SmokeTests.TestSuite;
using TextToSpeech.SmokeTests.Util;

namespace TextToSpeech.SmokeTests.TestDefinitions.TextToSpeech
{
	public class TextToSpeechSmokeTest : ISmokeTest
	{
		private readonly TextToSpeechServiceRestClient restClient;
		private readonly TtsRequest ttsRequest;

		public TextToSpeechSmokeTest(TextToSpeechServiceRestClient
			textToSpeechServiceRestClient, TtsRequest ttsRequest)
		{
			Name = "TextToSpeechSmokeTest";
			Description = "Will Try Make TTS From TTS Service";
			this.ttsRequest = ttsRequest;

			restClient = textToSpeechServiceRestClient;
		}

        public string Description { get; set; }
        public string Name { get; set; }

		public SmokeTestResult ExceuteTest()
		{
			try
			{
				return ExceuteTextToSpeechSmokeTest();
			}
			catch (Exception exception)
			{
				return FailedResult(exception.Message, exception.ToString());
			}
		}

		private SmokeTestResult ExceuteTextToSpeechSmokeTest()
		{
			var testResult = new SmokeTestResult();

			var sythizeResponse = restClient.SynthesizeText(ttsRequest);

			testResult.Success = sythizeResponse.Success;
			testResult.ErrorMessage = sythizeResponse.ErrorMessage;
			testResult.ErrorCode = sythizeResponse.ErrorCode;
			testResult.MoreInfo = sythizeResponse.MoreInfo;

			if (sythizeResponse.Success)
			{
				var resultInfoBuilder = new StringBuilder();
				resultInfoBuilder.AppendLine("");
				resultInfoBuilder.AppendLine(".......................");

				resultInfoBuilder.AppendLine("Response Audio Length      : " + sythizeResponse.Audio.Length);
				
				var fileName = Guid.NewGuid() + FileExtensionMapper.Map(ttsRequest.Audio.Format);

				var path = Path.Combine("SythizedAudio", fileName.AppendTimeStamp());

				var directory = Path.GetDirectoryName(path);

				Directory.CreateDirectory(directory);

				File.WriteAllBytes(path, sythizeResponse.Audio);

				resultInfoBuilder.AppendLine("Audio Data Written to File : " + path);

				testResult.ResultInfo = resultInfoBuilder.ToString();
			}
			

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