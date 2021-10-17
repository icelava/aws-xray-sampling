﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XRayFrontWeb.Models;

namespace XRayFrontWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Responsiveness()
		{
			// Simulate random slowless.
			var delayedTime = (new Random()).Next(2000);
			Task.Delay(delayedTime).Wait();

			ViewData["Timing"] = delayedTime + "ms";


			return View();
		}

		public IActionResult Reliability()
		{
			// Simulate random exception.
			var exceptionChance = (new Random()).Next(4);
			if (exceptionChance == 0) throw new Exception("Something's gone wrong 25% of the time.");

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
