using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace FootTrack.Api.SwaggerExamples.Responses
{
    public class ProblemDetailsExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            return new ProblemDetails
            {
                Detail = "Please provide correct parameter.",
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Type = "https://httpstatuses.com/400"
            };
        }
    }
}
