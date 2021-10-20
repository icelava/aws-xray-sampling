using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XRayFrontWeb.Configuration
{
	public class AwsConfig
	{
		public static AwsConfig Instance;
		public string Account { get; set; }
		public string Region { get; set; }
		public string AccessKeyId { get; set; }
		public string SecretAccessKey { get; set; }
		public string S3Bucket { get; set; }


	}
}
