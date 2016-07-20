using System;

namespace TextToSpeech.SmokeTests
{
	public static class ConsoleLogger
	{
		private static readonly object WriteLock = new object();

		public static void WriteLine(string text = "",
			params object[] parameters)
		{
			lock (WriteLock)
			{
				Console.WriteLine(text, parameters);
			}
		}

		public static void DisplayInfo(string errorMessage)
		{
			lock (WriteLock)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Cyan;
				WriteLine(errorMessage);

				Console.ForegroundColor = temp;
			}
		}

		public static void DisplayError(string errorMessage)
		{
			lock (WriteLock)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				WriteLine(errorMessage);

				Console.ForegroundColor = temp;
			}
		}

		

		public static void DisplayWarning(string warningMessage)
		{
			lock (WriteLock)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Yellow;
				WriteLine(warningMessage);

				Console.ForegroundColor = temp;
			}
		}

		public static void DisplaySuccess(string message)
		{
			lock (WriteLock)
			{
				var temp = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Green;
				WriteLine(message);

				Console.ForegroundColor = temp;
			}
		}
	}
}