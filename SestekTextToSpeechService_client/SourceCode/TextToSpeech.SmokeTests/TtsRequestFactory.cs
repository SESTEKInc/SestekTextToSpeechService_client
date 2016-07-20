using System.Collections.Generic;
using TextToSpeech.RestServiceContracts.Constants;
using TextToSpeech.RestServiceContracts.DataContracts;

namespace TextToSpeech.SmokeTests
{
	public class TtsRequestFactory
	{
		public TtsRequestFactory(Dictionary<string, string> license, string textToSynthesize)
		{
			License = license;
			TextToSynthesize = textToSynthesize;

			


		}

		public Dictionary<string, string> License { get; set; }
		public string TextToSynthesize { get; set; }

		public TtsRequest TtsRequestForWavFile
		{
			get
			{
				var voice = new VoiceInformation {Name = "GVZ Gul 16k_HV_Premium", Rate = 1.3, Volume = 1};
				var audio = new AudioInfo {Format = AudioFormat.Wav};

				audio.FormatDetails.Add(AudioInfoKey.Encoding, WavEncodingKey.Pcm);
				audio.FormatDetails.Add(AudioInfoKey.SampleRate, "8000");


				var ttsRequest = new TtsRequest
				{
					Text = TextToSynthesize,
					Voice = voice,
					Audio = audio,
					License = License
				};

				


				return ttsRequest;
			}
		}

		public TtsRequest TtsRequestForOpusFile
		{
			get
			{
				var voice = new VoiceInformation {Name = "GVZ Gul 16k_HV_Premium", Rate = 1, Volume = 1};
				var audio = new AudioInfo {Format = AudioFormat.Opus};

				audio.FormatDetails.Add(AudioInfoKey.BitRateKbps, "8");
				audio.FormatDetails.Add(AudioInfoKey.SampleRate, "8000");


				var ttsRequest = new TtsRequest
				{
					Text = TextToSynthesize,
					Voice = voice,
					Audio = audio,
					License = License
				};

				return ttsRequest;
			}
		}

		public TtsRequest TtsRequestForMp3File
		{
			get
			{
				var voice = new VoiceInformation {Name = "GVZ Gul 16k_HV_Premium", Rate = 1, Volume = 1};
				var audio = new AudioInfo {Format = AudioFormat.Mp3};

				audio.FormatDetails.Add(AudioInfoKey.BitRateKbps, "8");
				audio.FormatDetails.Add(AudioInfoKey.SampleRate, "8000");


				var ttsRequest = new TtsRequest
				{
					Text = TextToSynthesize,
					Voice = voice,
					Audio = audio,
					License = License
				};

				return ttsRequest;
			}
		}

		public TtsRequest TtsRequestForFlvFile
		{
			get
			{
				var voice = new VoiceInformation {Name = "GVZ Gul 16k_HV_Premium", Rate = 1, Volume = 1};
				var audio = new AudioInfo {Format = AudioFormat.Flv};

				audio.FormatDetails.Add(AudioInfoKey.BitRateKbps, "8");
				audio.FormatDetails.Add(AudioInfoKey.SampleRate, "8000");


				var ttsRequest = new TtsRequest
				{
					Text = TextToSynthesize,
					Voice = voice,
					Audio = audio,
					License = License
				};

				return ttsRequest;
			}
		}
	}
}