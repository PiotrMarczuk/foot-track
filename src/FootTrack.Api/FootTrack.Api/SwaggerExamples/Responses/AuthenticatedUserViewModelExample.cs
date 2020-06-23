using FootTrack.Api.ViewModels;

using Swashbuckle.AspNetCore.Filters;

namespace FootTrack.Api.SwaggerExamples.Responses
{
    public class AuthenticatedUserViewModelExample : IExamplesProvider<AuthenticatedUserViewModel>
    {
        public AuthenticatedUserViewModel GetExamples()
        {
            return new AuthenticatedUserViewModel
            {
                Email = "testuser@gmail.com",
                FirstName = "Jan",
                LastName = "Bąk",
                Id = "5ef0d2620665d96624629b80",
                Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjVlZjBkMjYyMDY2NWQ5NjYyNDYyOWI4MCIsIm5iZiI6MTU5Mjg1NzM0MiwiZXhwIjoxNTky" +
                        "ODY0NTQyLCJpYXQiOjE1OTI4NTczNDJ9.oHE4p9j4QitElstGRFv60Fzh5rYMv-fse697tq-ml1ITaGkqhLSL5pp3GYsfbHR3vMsxiEUJu91xfOyOT1zIpQ"
            };
        }
    }
}
