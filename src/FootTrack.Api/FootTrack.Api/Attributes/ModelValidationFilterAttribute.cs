using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FootTrack.Api.Attributes
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
