using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HrApp.Services
{
    public class IdentityRepository : IdentityRepositoryInterface
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        public IdentityRepository(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password)
        {
            var result = await GetIdentityUserAsync(username, email, password);
            if (result.Succeeded)
                result = await LoginAsync(result.IdentityUser, password);
            return result;
        }

        async Task<IdentityRepositoryResult> LoginAsync(IdentityUser user, string password)
        {
            var result = new IdentityRepositoryResult();
            var identityUserResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (identityUserResult.Succeeded)
                result.Succeeded = true;
            else
                result.AddError("Error met inloggen");
            return result;
        }

        public async Task<IdentityRepositoryResult> GetIdentityUserAsync(string username, string email, string password)
        {
            var result = new IdentityRepositoryResult();
            IdentityUser user = null;
            if (username != null)
            {
                user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    result.AddError("User with this username does not exist");
                }
            }
            else
            {
                if (email != null)
                {
                    user = await _userManager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        result.AddError("User with this email does not exist");
                    }
                }
            }

            if (user != null)
            {
                result.IdentityUser = user;
                var identityUserResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (identityUserResult.Succeeded)
                {
                    result.Succeeded = true;
                }
            }

            return result;
        }
        public async Task<IdentityRepositoryResult> RegisterAsync(RegisterViewModel register)
        {
            var result = new IdentityRepositoryResult();
            var identityUser = new IdentityUser() { UserName = register.UserName, Email = register.Email };
            var identityResult = await _userManager.CreateAsync(identityUser, register.Password);
            if (identityResult.Succeeded)
            {
                result.Succeeded = true;
            }
            else
            {
                foreach (var error in result.Errors)
                    result.AddError(error);
            }

            return result;
        }

        public void SignOutAsync()
        {
            _signInManager.SignOutAsync();
        }

    }
}
