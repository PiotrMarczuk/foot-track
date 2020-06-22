using FootTrack.Api.ViewModels;

using Swashbuckle.AspNetCore.Filters;

namespace FootTrack.Api.SwaggerExamples.Requests
{
    public class UserRegisterViewModelExample : IExamplesProvider<UserRegisterViewModel>
    {
        public UserRegisterViewModel GetExamples()
        {
            return new UserRegisterViewModel
            {
                Email = "testuser@gmail.com",
                FirstName = "Jan",
                LastName = "Bąk",
                Password = "superExtraPassword2134"
            };
        }
    }
}
