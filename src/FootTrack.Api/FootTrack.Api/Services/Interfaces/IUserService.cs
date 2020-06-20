using System.Threading.Tasks;

using FootTrack.Api.Models;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> AuthenticateAsync(UserLoginViewModel loginViewModel);

        Task<User> GetAsync(string id);

        Task<User> CreateAsync(UserRegisterViewModel registerViewModel);
    }
}
