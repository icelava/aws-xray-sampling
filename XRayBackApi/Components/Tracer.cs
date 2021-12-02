﻿using Amazon.XRay.Recorder.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XRayBackApi.Configuration;

namespace XRayBackApi.Components
{
	public class Tracer
	{
		public static void InitRequest()
		{
			ApplySystemAnnotation();
		}
		private static void ApplySystemAnnotation()
		{
			AWSXRayRecorder.Instance.AddAnnotation("System", AwsConfig.Instance.XRay.System);
		}
	}
}
