using FootTrack.Api.ViewModels;

using Swashbuckle.AspNetCore.Filters;

namespace FootTrack.Api.SwaggerExamples.Requests
{
    public class UserLoginViewModelExample : IExamplesProvider<UserLoginViewModel>
    {
        public UserLoginViewModel GetExamples()
        {
            return new UserLoginViewModel
            {
                Email = "testuser@gmail.com",
                Password = "superExtraPassword2134"
            };
        }
    }
}
