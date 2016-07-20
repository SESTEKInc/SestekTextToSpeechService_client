using System.Collections.Generic;
using TextToSpeech.RestServiceContracts.DataContracts;

namespace TextToSpeech.RestServiceContracts.ServiceConracts
{
	public interface ISestekSpeechSynthesizer
	{
		List<SynthesizerVoice> AvaliableVoices();
		List<SynthesizerVoice> AvaliableVoices(string gender, string culture);

		byte[] Tts(string text, VoiceInformation voice, AudioInfo audioInfo, out int durationMilliSec);

		int CalculateCharacterCount(string text);

	}
}
