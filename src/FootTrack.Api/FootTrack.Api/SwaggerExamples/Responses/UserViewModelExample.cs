using FootTrack.Api.ViewModels;

using Swashbuckle.AspNetCore.Filters;

namespace FootTrack.Api.SwaggerExamples.Responses
{
    public class UserViewModelExample : IExamplesProvider<UserViewModel>
    {
        public UserViewModel GetExamples()
        {
            return new UserViewModel
            {
                Email = "testuser@gmail.com",
                FirstName = "Jan",
                LastName = "Bąk",
                Id = "5ef0d2620665d96624629b80"
            };
        }
    }
}
