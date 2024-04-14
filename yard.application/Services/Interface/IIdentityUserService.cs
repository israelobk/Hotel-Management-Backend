using Microsoft.AspNetCore.Identity;
using yard.domain.Models;
using yard.domain.ViewModels;

namespace yard.application.Services.Interface
{
	public interface IIdentityUserService
	{
		Task<AppUser> FindUser(string email);
		Task<IdentityResult> RegisterUser(string password, AppUser user);
		Task<IdentityResult> CreateRole(IdentityRole<int> role);

		Task<bool> CheckRole(string roleName);

		AppUser CreateUserFromModel(RegistrationVM model);

		string GetErrorsFromIdentityResult(IdentityResult result);

		Task AssignRolesToUser(AppUser user);

		Task<IdentityResult> AddToRole(AppUser user, string role);

		Task<bool> CheckPassword(AppUser user, string password);

		Task<IList<string>> GetRoles(AppUser user);

		Task<string> Login(LoginVM request);

		Task<List<AppUser>> GetRegisteredUsers();

		//Task<bool> UserExists( username);
		Task<AppUser> GetUserById(int Id);
		Task<bool> UserExistAsync(int AppUserId);
	}
}