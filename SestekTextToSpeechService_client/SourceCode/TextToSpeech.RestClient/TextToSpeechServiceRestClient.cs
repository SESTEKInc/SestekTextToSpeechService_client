using System;
using RestSharp;
using TextToSpeech.RestClient.Extensions;
using TextToSpeech.RestClient.Serilization;
using TextToSpeech.RestServiceContracts.DataContracts;

namespace TextToSpeech.RestClient
{
	public class TextToSpeechServiceRestClient
	{
		public TextToSpeechServiceRestClient(string serviceBaseUrl, string ttsRoute,
			string voicesRoute)
		{
			ServiceBaseUrl = serviceBaseUrl;
			TtsRoute = ttsRoute;
			VoicesRoute = voicesRoute;
		}

        public string ServiceBaseUrl { get; set; }
        public string TtsRoute { get; set; }
        public string VoicesRoute { get; set; }
		public string Info { get; set; }

		public SynthesizerVoicesResponse GetAvaliableVoices()
		{
			try
			{
				var client = NewRestClient();

				IRestRequest restRequest = new RestRequest(VoicesRoute, Method.GET);

				var restResponse = client.Execute<SynthesizerVoicesResponse>(restRequest);


				if (restResponse.Failed())
				{
					return FailedSynthesizerVoiceListResponse(restResponse.FailureReason(),
						restResponse.FailureExceptionMessage());
				}

				return restResponse.Data;
			}
			catch (Exception exception)
			{
				return FailedSynthesizerVoiceListResponse(exception.Message,
					exception.ToString());
			}
		}

		public TtsResponse SynthesizeText(TtsRequest ttsRequest)
		{
			try
			{
				var client = NewRestClient();
			
				IRestRequest restRequest = new RestRequest(TtsRoute, Method.POST);
				restRequest.RequestFormat = DataFormat.Json;
				restRequest.JsonSerializer = NewtonsoftJsonSerializer.Default;
			
				restRequest.AddBody(ttsRequest);

			
				IRestResponse restResponse = client.Execute(restRequest);

				
				if (restResponse.Failed())
				{
					return FailedTtsResponse(restResponse.FailureReason(),
						restResponse.FailureExceptionMessage());
				}

				var ttsResponse = new TtsResponse
				{
					Audio = restResponse.RawBytes,
					Success = true
				};

				return ttsResponse;
			}
			catch (Exception exception)
			{
				
				var failedTttsResponse = FailedTtsResponse(exception.Message,
					exception.ToString());
				return failedTttsResponse;
			}
		}

		private RestSharp.RestClient NewRestClient()
		{
			var client = new RestSharp.RestClient {UserAgent = Info};
			client.AddDefaultHeader("Accept-charset", "utf-8");
			client.BaseUrl = new Uri(ServiceBaseUrl);
			client.Timeout = 60*1000*10;
			return client;
		}

		private TtsResponse FailedTtsResponse(string failureReason, string moreInfo = "")
		{
			var failedTtsResponse = new TtsResponse
			{
				Success = false,
				ErrorMessage = failureReason,
				MoreInfo = moreInfo
			};

			return failedTtsResponse;
		}

		private static SynthesizerVoicesResponse FailedSynthesizerVoiceListResponse(string errorMessage,
			string moreInfo = "")
		{
			var failedSynthesizerVoiceListResponse = new SynthesizerVoicesResponse
			{
				Success = false,
				ErrorMessage = errorMessage,
				MoreInfo = moreInfo
			};
			return failedSynthesizerVoiceListResponse;
		}
	}
}