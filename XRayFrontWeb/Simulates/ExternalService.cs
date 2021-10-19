using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XRayFrontWeb.Simulates
{
    public class ExternalService
    {
		public static async Task<int> DelayRandomly()
		{
			var delayedTime = (new Random()).Next(1500);
			if (delayedTime < 0) await Task.Delay(delayedTime);

			return delayedTime;
		}
    }
}
