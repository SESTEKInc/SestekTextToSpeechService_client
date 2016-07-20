using System;
using System.Linq;
using System.Net;
using RestSharp;

namespace TextToSpeech.RestClient.Extensions
{
	public static class RestSharpExtensionMethods
	{
		public static string HeaderValue(this IRestResponse restResponse, string headerName)
		{
			var headerValue = string.Empty;

			var firstOrDefault = restResponse.Headers.FirstOrDefault(t => t.Name.Equals(headerName,
				StringComparison.OrdinalIgnoreCase));

			if (firstOrDefault != null)
			{
				headerValue = firstOrDefault.Value.ToString();
			}


			return headerValue;
		}

		public static string HeaderValue(this IRestResponse<TimeSpan> restResponse, string headerName)
		{
			var headerValue = string.Empty;

			var firstOrDefault = restResponse.Headers.FirstOrDefault(t => t.Name.Equals(headerName,
				StringComparison.OrdinalIgnoreCase));

			if (firstOrDefault != null)
			{
				headerValue = firstOrDefault.Value.ToString();
			}


			return headerValue;
		}

		public static bool Successful(this IRestResponse response)
		{
			var success = (response.StatusCode.IsScuccessStatusCode())
			              && (response.ResponseStatus == ResponseStatus.Completed);

			return success;
		}

		public static bool Successful<T>(this IRestResponse<T> response)
		{
			var success = (response.StatusCode.IsScuccessStatusCode())
			              && (response.ResponseStatus == ResponseStatus.Completed)
			              && (response.Data != null);


			return success;
		}

		public static bool Failed(this IRestResponse response)
		{
			return (!Successful(response));
		}

		public static bool Failed<T>(this IRestResponse<T> response)
		{
			return (!Successful(response));
		}

		public static string FailureReason<T>(this IRestResponse<T> response)
		{
			if (response.Successful())
			{
				return string.Empty;
			}

			var errorMessage = string.Empty;
			errorMessage += "Failed Response." + Environment.NewLine;
			errorMessage += "StatusCode             : " + response.StatusCode + "-" + (int) response.StatusCode +
			                Environment.NewLine;
			errorMessage += "StatusDescription      : " + response.StatusDescription + Environment.NewLine;


			if (response.ErrorException != null)
			{
				errorMessage += "RestSharpException     : " + response.ErrorException.Message + Environment.NewLine;
			}

			return errorMessage;
		}

		public static string FailureReason(this IRestResponse response)
		{
			if (response.Successful())
			{
				return string.Empty;
			}


			var errorMessage = string.Empty;
			errorMessage += "Failed Response." + Environment.NewLine;
			errorMessage += "StatusCode             : " + response.StatusCode + "-" + (int) response.StatusCode +
			                Environment.NewLine;
			errorMessage += "StatusDescription      : " + response.StatusDescription + Environment.NewLine;


			if (response.ErrorException != null)
			{
				errorMessage += "RestSharpException     : " + response.ErrorException.Message + Environment.NewLine;
			}

			return errorMessage;
		}

		public static string FailureExceptionMessage<T>(this IRestResponse<T> response)
		{
			var moreInfo = string.Empty;

			if (response.ErrorException != null)
			{
				moreInfo = "RestSharp  Exception :" + response.ErrorException;
			}

			return moreInfo;
		}

		public static string FailureExceptionMessage(this IRestResponse response)
		{
			var moreInfo = string.Empty;

			if (response.ErrorException != null)
			{
				moreInfo = "RestSharp  Exception :" + response.ErrorException;
			}

			return moreInfo;
		}

		private static bool IsScuccessStatusCode(this HttpStatusCode responseCode)
		{
			var numericResponse = (int) responseCode;
			return numericResponse >= 200
			       && numericResponse <= 399;
		}
	}
}