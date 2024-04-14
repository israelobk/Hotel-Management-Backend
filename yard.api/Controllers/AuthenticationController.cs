using Microsoft.AspNetCore.Mvc;
using yard.application.Services.Interface;
using yard.domain.Models;
using yard.domain.ViewModels;

namespace yard.api.Controllers
{
	public class AuthenticationController : ControllerBase
	{
		private readonly IIdentityUserService _identityUser;

		public AuthenticationController(IIdentityUserService identityUser)
		{
			_identityUser = identityUser;
		}

		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Register([FromBody] RegistrationVM model)
		{
			if (model.ConfirmPassword != model.Password)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}

			var userExists = await _identityUser.FindUser(model.Email);

			if (userExists != null)
				return StatusCode(StatusCodes.Status409Conflict);
			AppUser user = _identityUser.CreateUserFromModel(model);

			var result = await _identityUser.RegisterUser(model.Password, user);

			if (!result.Succeeded)
			{
				string errors = _identityUser.GetErrorsFromIdentityResult(result);

				return StatusCode(StatusCodes.Status400BadRequest);
			}

			return Ok();
		}

		[HttpPost("register-admin")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status409Conflict)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegistrationVM model)
		{
			if (model.ConfirmPassword != model.Password)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}

			var userExists = await _identityUser.FindUser(model.Email);

			if (userExists != null)
				return StatusCode(StatusCodes.Status409Conflict);
			AppUser user = _identityUser.CreateUserFromModel(model);

			var result = await _identityUser.RegisterUser(model.Password, user);

			if (!result.Succeeded)
			{
				string errors = _identityUser.GetErrorsFromIdentityResult(result);

				return StatusCode(StatusCodes.Status400BadRequest);
			}

			await _identityUser.AssignRolesToUser(user);

			return Ok();
		}


		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] LoginVM loginModel)
		{
			var token = await _identityUser.Login(loginModel);
			if (token == null)
			{
				return Unauthorized();
			}


			return Ok(token);
		}

		[HttpGet("RegisteredUsers")]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetRegisteredUsers()
		{
			return Ok(await _identityUser.GetRegisteredUsers());
		}

		[HttpGet("RegisteredUserById")]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetRegisteredUserById(int AppUserId)
		{
			var userExist = await _identityUser.UserExistAsync(AppUserId);
			if (userExist == false)
			{
				return NotFound($"User Id {AppUserId} does not exist");
			}

			return Ok(await _identityUser.GetUserById(AppUserId));
		}
	}
}