using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.SmokeTests.TestSuite
{
	public interface ISmokeTest
	{
		string Description { get; }
		string Name { get; }
		SmokeTestResult ExceuteTest();
	}
}
