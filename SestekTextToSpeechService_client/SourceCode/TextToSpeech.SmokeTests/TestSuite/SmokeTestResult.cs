using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.SmokeTests.TestSuite
{
	public class SmokeTestResult
	{
		public bool Success { get; set; }
		public string ErrorMessage { get; set; }
		public string ErrorCode { get; set; }
		public string MoreInfo { get; set; }
		public string ResultInfo { get; set; }
	}
}
