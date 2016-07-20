using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextToSpeech.RestClient;

namespace TextToSpeech.SmokeTests
{
	public class RestClientFactory
	{
		public RestClientFactory(string serviceBaseUrl, string ttsRoute,
			string voicesRoute)
		{
			ServiceBaseUrl = serviceBaseUrl;
			TtsRoute = ttsRoute;
			VoicesRoute = voicesRoute;
		}

		public TextToSpeechServiceRestClient NewRestClient
		{
			get
			{
				return new TextToSpeechServiceRestClient(ServiceBaseUrl, TtsRoute, VoicesRoute);
			}
		}

		private string ServiceBaseUrl { get; set; }
		private string TtsRoute { get; set; }
		private string VoicesRoute { get; set; }
	}
}
