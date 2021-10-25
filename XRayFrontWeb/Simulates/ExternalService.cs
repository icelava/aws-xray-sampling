using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace XRayFrontWeb.Simulates
{
    public class ExternalService
    {
		public static HttpClient GetTracingHttpClient()
		{
			return new HttpClient(new HttpClientXRayTracingHandler(new HttpClientHandler()));
		}
		public static async Task<int> DelayRandomly()
		{
			AWSXRayRecorder.Instance.BeginSubsegment("DelayRandomlyService");
			var delayedTime = (new Random()).Next(1500);
			// If no delay then exit immediately without delay action.
			if (delayedTime == 0)
			{
				AWSXRayRecorder.Instance.EndSubsegment();
				return delayedTime;
			}

			await Task.Delay(delayedTime).ContinueWith(t =>
			{
				AWSXRayRecorder.Instance.EndSubsegment();
			});

			
			return delayedTime;
		}
    }
}
