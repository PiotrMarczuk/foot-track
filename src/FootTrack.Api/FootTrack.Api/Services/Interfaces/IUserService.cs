using System.Threading.Tasks;

using FootTrack.Api.Models;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticatedUserViewModel> AuthenticateAsync(UserLoginViewModel loginViewModel);

        Task<User> GetByIdAsync(string id);

        Task<User> CreateAsync(UserRegisterViewModel registerViewModel);
    }
}
