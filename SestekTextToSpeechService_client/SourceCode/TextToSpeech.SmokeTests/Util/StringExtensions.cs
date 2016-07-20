using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSpeech.SmokeTests.Util
{
	public static class StringExtensions
	{
		public static string AppendTimeStamp(this string fileName)
		{
			return string.Concat(
				Path.GetFileNameWithoutExtension(fileName),
				"__",
				DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"),
				Path.GetExtension(fileName)
				);
		}
	}
}
