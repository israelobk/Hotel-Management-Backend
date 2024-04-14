using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using yard.api.Utility;
using yard.application.Services.Interface;
using yard.domain.Context;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Utility;

namespace yard.application.Services.Implementation
{
	public class IdentityUserService : IIdentityUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole<int>> _roleManager;
		private readonly IConfiguration _configuration;
		private readonly ApplicationContext _context;

		public IdentityUserService(UserManager<AppUser> userManager, RoleManager<IdentityRole<int>> roleManager,
			IConfiguration configuration, ApplicationContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
			_context = context;
		}

		public async Task<AppUser> FindUser(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			return user;
		}

		public async Task<bool> CheckRole(string roleName)
		{
			return await _roleManager.RoleExistsAsync(roleName);
		}

		public async Task<IdentityResult> RegisterUser(string password, AppUser user)
		{
			return await _userManager.CreateAsync(user, password);
		}

		public async Task<IdentityResult> CreateRole(IdentityRole<int> role)
		{
			return await _roleManager.CreateAsync(role);
		}

		public async Task<IdentityResult> AddToRole(AppUser user, string roleName)
		{
			return await _userManager.AddToRoleAsync(user, "Admin");
		}

		public AppUser CreateUserFromModel(RegistrationVM model)
		{
			return new AppUser()
			{
				Email = model.Email,
				UserName = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				ProfilePictureUrl = model.ProfilePictureUrl,
				Address = new Address
				{
					City = model.Address.City,
					State = model.Address.State,
					Street = model.Address.Street,
					PostalCode = model.Address.PostalCode,
					Country = model.Address.Country
				},
			};
		}

		public string GetErrorsFromIdentityResult(IdentityResult result)
		{
			string errors = string.Empty;

			foreach (var error in result.Errors)
			{
				errors += error.Description;
				errors += "@";
			}

			errors = errors.Replace("@", System.Environment.NewLine);

			return errors;
		}

		public async Task AssignRolesToUser(AppUser user)
		{
			foreach (var roleName in new[] { UserRoles.Admin, UserRoles.User })
			{
				if (!await CheckRole(roleName.ToString()))
				{
					await CreateRole(new IdentityRole<int>(roleName));
				}
			}

			if (await CheckRole(UserRoles.Admin))
			{
				await AddToRole(user, UserRoles.Admin);
			}
		}

		public async Task<bool> CheckPassword(AppUser user, string password)
		{
			var isValid = await _userManager.CheckPasswordAsync(user, password);

			return isValid;
		}

		public async Task<IList<string>> GetRoles(AppUser user)
		{
			return await _userManager.GetRolesAsync(user);
		}

		public async Task<string> Login(LoginVM request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				var claim = ClaimsImplentation.GetClaims(userRoles, user.UserName);
				//JWT
				string token = JwtImplementation.GetJWTToken(_configuration, claim);

				return token;
			}

			return null;
		}

		public async Task<List<AppUser>> GetRegisteredUsers()
		{
			return await _userManager.Users.ToListAsync();
		}

		public async Task<AppUser> GetUserById(int Id)
		{
			var userIdAsString = Id.ToString();
			var user = await _userManager.FindByIdAsync(userIdAsString);
			return user;
		}

		public async Task<bool> UserExistAsync(int AppUserId)
		{
			return await _context.Users.AnyAsync(a => a.Id == AppUserId);
		}
	}
}