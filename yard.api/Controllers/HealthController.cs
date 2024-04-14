using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace yard.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HealthController : ControllerBase
	{
		[HttpGet]
		public void GetError()
		{
			throw new InvalidOperationException();
		}
	}
}