using HrApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HrApp.Services
{
    public interface IdentityRepositoryInterface
    {
        Task<IdentityRepositoryResult> LoginAsync(string username, string email, string password);
        Task<IdentityRepositoryResult> RegisterAsync(RegisterViewModel register);
        void SignOutAsync();
    }

}
