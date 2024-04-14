using Microsoft.AspNetCore.Mvc;
using yard.domain.ViewModels;

namespace yard.api.Controllers
{

    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
/*        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]*/
        [NonAction]
        public IActionResult Response (ApiResponse response)
        {
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }

            if (response.StatusCode == 404)
            {
                return NotFound(response);
            }

            return StatusCode(response.StatusCode, response);
        }
    }
}
