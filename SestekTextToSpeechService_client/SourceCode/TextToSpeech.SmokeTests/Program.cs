using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using TextToSpeech.SmokeTests.TestDefinitions.AvaliableVoices;
using TextToSpeech.SmokeTests.TestDefinitions.TextToSpeech;
using TextToSpeech.SmokeTests.TestSuite;

namespace TextToSpeech.SmokeTests
{
	internal class Program
	{
		private string ServiceBaseUrl { get; set; }
		private string TtsRoute { get; set; }
		private string VoicesRoute { get; set; }
		private bool UseLicense { get; set; }
		private bool UseSsl { get; set; }
		private Dictionary<string, string> License { get; set; }

		private static void Main(string[] args)
		{
			var program = new Program();

			program.Start();

			ConsoleLogger.DisplayInfo("Press Any Key To Exit!");
			Console.ReadLine();
		}

		private void Start()
		{
			try
			{
				EncrypteConfiguration();

				ReadConfiguration();

				DecrypteLicenseFromConfiguration();

				DisplaySmokeTestDescription();

				DisplayConfiguration();

				RunSmokeTests();
			}
			catch (Exception exception)
			{
				ConsoleLogger.DisplayError("Smoke Test Error.");
				ConsoleLogger.DisplayError(exception.ToString());
			}
		}

		public void RunSmokeTests()
		{
			var textToSynthesize = "Bugün Hava Çok Güzel.";

			var restClientFactory = new RestClientFactory(ServiceBaseUrl, TtsRoute, VoicesRoute);
			var ttsRequestFactory = new TtsRequestFactory(License, textToSynthesize);


			var avaliableVoicesSmokeTest = new AvaliableVoicesSmokeTest(restClientFactory.NewRestClient);

			var ttstForWavSmokeTest = new TextToSpeechSmokeTest(restClientFactory.NewRestClient,
				ttsRequestFactory.TtsRequestForWavFile);
			var ttstForOpusSmokeTest = new TextToSpeechSmokeTest(restClientFactory.NewRestClient,
				ttsRequestFactory.TtsRequestForOpusFile);
			var ttstForMp3SmokeTest = new TextToSpeechSmokeTest(restClientFactory.NewRestClient,
				ttsRequestFactory.TtsRequestForMp3File);
			var ttstForFlvSmokeTest = new TextToSpeechSmokeTest(restClientFactory.NewRestClient,
				ttsRequestFactory.TtsRequestForFlvFile);

			var smokeTestsSuite = new SmokeTestsSuite();

			smokeTestsSuite.AddTest(avaliableVoicesSmokeTest);

			smokeTestsSuite.AddTest(ttstForWavSmokeTest);
			smokeTestsSuite.AddTest(ttstForOpusSmokeTest);
			smokeTestsSuite.AddTest(ttstForMp3SmokeTest);
			smokeTestsSuite.AddTest(ttstForFlvSmokeTest);

			//smokeTestsSuite.ShuffleTestSuite();
			smokeTestsSuite.ExecuteTestSequentially();
		}

		public void ReadConfiguration()
		{
			UseSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSSL"]);
			UseLicense = Convert.ToBoolean(ConfigurationManager.AppSettings["UseLicense"]);

			ServiceBaseUrl = UseSsl
				? ConfigurationManager.AppSettings["TtsServiceSSLBaseUrl"]
				: ConfigurationManager.AppSettings["TtsServiceBaseUrl"];

			TtsRoute = ConfigurationManager.AppSettings["TtsRoute"];
			VoicesRoute = ConfigurationManager.AppSettings["VoicesRoute"];
		}

		public void DisplayConfiguration()
		{
			var messageBuilder = new StringBuilder();
			messageBuilder.AppendLine("");
			messageBuilder.AppendLine("> Smoke Tests Configuration");
			messageBuilder.AppendLine("");

			messageBuilder.AppendLine("     UseSsl                 : " + UseSsl);
			messageBuilder.AppendLine("     UseLicense             : " + UseLicense);
			messageBuilder.AppendLine("     ServiceBaseUrl         : " + ServiceBaseUrl);
			messageBuilder.AppendLine("     TtsRoute               : " + TtsRoute);
			messageBuilder.AppendLine("     VoicesRoute            : " + VoicesRoute);
			messageBuilder.AppendLine("     Is64BitOperatingSystem : " + Environment.Is64BitOperatingSystem);
			messageBuilder.AppendLine("     Is64BitProcess         : " + Environment.Is64BitProcess);
			messageBuilder.AppendLine("     OSVersion              : " + Environment.OSVersion);

			messageBuilder.AppendLine("");
			messageBuilder.AppendLine("");
			ConsoleLogger.DisplayWarning(messageBuilder.ToString());
		}

		private void DisplaySmokeTestDescription()
		{
			var messageBuilder = new StringBuilder();

			messageBuilder.AppendLine("Welcome To TTS Rest Service Smoke Tests!");
			messageBuilder.AppendLine("");
			messageBuilder.AppendLine("> What Is Smoke Test?");
			messageBuilder.AppendLine("");
			messageBuilder.AppendLine("<< Smoke testing refer to a first - pass, ");
			messageBuilder.AppendLine("   shallow form of testing intended to establish whether ");
			messageBuilder.AppendLine("   a product or system can perform the most basic functions.");
			messageBuilder.AppendLine("   Smoke tests verifies that the deployment worked and the application is running.");
			messageBuilder.AppendLine("   Called also Post Deployment testing >>");
			messageBuilder.AppendLine("");

			ConsoleLogger.DisplayInfo(messageBuilder.ToString());
		}

		private void EncrypteConfiguration()
		{
			var appName = "TextToSpeech.SmokeTests.exe";

			var config = ConfigurationManager.OpenExeConfiguration(appName);
			var section = config.GetSection("licenseInfo").SectionInformation;

			if (section.IsProtected)
			{
				section.UnprotectSection();
			}
			else
			{
				section.ProtectSection("DataProtectionConfigurationProvider");
				section.ForceSave = true;
			}
			config.Save(ConfigurationSaveMode.Full);
		}

		private void DecrypteLicenseFromConfiguration()
		{
			if (!UseLicense)
			{
				License = null;
				return;
			}

			var licenseInfo = ConfigurationManager.GetSection("licenseInfo") as NameValueCollection;
			var licenseITems = licenseInfo.AllKeys.SelectMany(licenseInfo.GetValues, (k, v) => new {key = k, value = v});


			License = new Dictionary<string, string>();
			foreach (var license in licenseITems)
			{
				License.Add(license.key, license.value);
			}
		}
	}
}