using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextToSpeech.SmokeTests.TestSuite
{
	public class SmokeTestsSuite
	{
		private readonly List<ISmokeTest> testSuiteToRun = new List<ISmokeTest>();
		private int failureCount;
		private int successCount;

		public List<ISmokeTest> AvaliableTests
		{
			get
			{
				return testSuiteToRun;
			}
		}

		public void IncreaseAvaliableTestsByFactor(int n)
		{
			if (n <= 0)
			{
				throw new ApplicationException("Invalid Scale Factor " + n);
			}

			var originalTest = AvaliableTests;

			for (var i = 1; i < n; i++)
			{
				AvaliableTests.AddRange(originalTest);
			}
		}

		public void AddTest(ISmokeTest smokeTest)
		{
			testSuiteToRun.Add(smokeTest);
		}

		public void ExecuteTestsParallel()
		{
			ParallelExecution();
		}

		public void ExecuteTestSequentially()
		{
			SequentiallExecution();
		}

		public void ShuffleTestSuite()
		{
			ShuffleList(testSuiteToRun);
		}

		private void ParallelExecution()
		{
			Parallel.ForEach(AvaliableTests, test =>
			{
				var result = test.ExceuteTest();

				if (result.Success)
				{
					Interlocked.Increment(ref successCount);
				}

				else
				{
					Interlocked.Increment(ref failureCount);
				}

				DisplayResult(result, test);
			});

			DisplayTotalResult();
		}

		private void SequentiallExecution()
		{
			foreach (var test in AvaliableTests)
			{
				var result = test.ExceuteTest();

				if (result.Success)
				{
					Interlocked.Increment(ref successCount);
				}

				else
				{
					Interlocked.Increment(ref failureCount);
				}

				DisplayResult(result, test);
			}

			DisplayTotalResult();
		}

		private static void DisplayResult(SmokeTestResult result, ISmokeTest test)
		{
			var messageBuilder = new StringBuilder();
			messageBuilder.AppendLine("Test Name        : " + test.Name);
			messageBuilder.AppendLine("Test Description : " + test.Description);


			if (result.Success)
			{
				messageBuilder.AppendLine("Test Status      : PASSED");
				messageBuilder.AppendLine("Result Info      : " + result.ResultInfo);
				messageBuilder.AppendLine("----------------------------------------------");

				ConsoleLogger.DisplaySuccess(messageBuilder.ToString());
			}

			else
			{
				messageBuilder.AppendLine("Test Status      : FAILED");
				messageBuilder.AppendLine("Error Message    : " + result.ErrorMessage);
				messageBuilder.AppendLine("Error Code       : " + result.ErrorCode);
				messageBuilder.AppendLine("More Info        : " + result.MoreInfo);
				messageBuilder.AppendLine("----------------------------------------------");

				ConsoleLogger.DisplayError(messageBuilder.ToString());
			}
		}

		private void DisplayTotalResult()
		{
			if (failureCount > 0)
			{
				ConsoleLogger.DisplayError("..................................................");
				ConsoleLogger.DisplayError("..................................................");
				ConsoleLogger.DisplayError("> FAILURE.....!!!");
				ConsoleLogger.DisplayError("> TOTAL NUMBER OF FAILED TESTS : " + failureCount);
				ConsoleLogger.DisplayError("> TOTAL NUMBER OF PASSED TESTS : " + successCount);
				ConsoleLogger.DisplayError("..................................................");
				ConsoleLogger.DisplayError("..................................................");
			}
			else
			{
				ConsoleLogger.DisplaySuccess("..................................................");
				ConsoleLogger.DisplaySuccess("..................................................");
				ConsoleLogger.DisplaySuccess("> ALL TEST PASSED....!!!!");
				ConsoleLogger.DisplaySuccess("> TOTAL NUMBER OF PASSED TESTS : " + successCount);
				ConsoleLogger.DisplaySuccess("..................................................");
			}
		}

		private void ShuffleList<T>(IList<T> list)
		{
			var provider = new RNGCryptoServiceProvider();

			var n = list.Count;
			while (n > 1)
			{
				var box = new byte[1];
				do provider.GetBytes(box); while (!(box[0] < n*(byte.MaxValue/n)));
				var k = (box[0]%n);
				n--;
				var value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}