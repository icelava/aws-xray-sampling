using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XRayBackApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TierController : ControllerBase
	{
		// GET: api/<TierController>
		[HttpGet]
		public async Task<int> Get()
		{
			var delayedTime = (new Random()).Next(1500);
			if (delayedTime == 0) return delayedTime;

			await Task.Delay(delayedTime);

			return delayedTime;
		}

		// GET api/<TierController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<TierController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<TierController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<TierController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
