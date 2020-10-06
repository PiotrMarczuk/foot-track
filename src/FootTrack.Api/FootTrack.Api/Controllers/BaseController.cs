using AutoMapper;
using FootTrack.Api.Utils;
using FootTrack.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootTrack.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IMapper _mapper;

        protected BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected IActionResult OkOrError<TDestination, TSource>(Result<TSource> result)
        {
            return result.IsSuccess
                ? Ok(_mapper.Map<TDestination>(result.Value))
                : HandleError(result);
        }

        protected IActionResult OkOrError(Result result)
        {
            return result.IsSuccess
                ? Ok()
                : HandleError(result);
        }

        protected IActionResult CreatedAtOrError<TDestination, TSource>(
            Result<TSource> result,
            string actionName,
            object routeValues)
        {
            return result.IsSuccess
                ? CreatedAtAction(actionName, routeValues, _mapper.Map<TDestination>(result.Value))
                : HandleError(result);
        }

        protected IActionResult Error(Result result)
        {
            return HandleError(result);
        }

        private IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        private IActionResult CreatedAtAction<T>(string actionName, object routeValues, T result)
        {
            return base.CreatedAtAction(actionName, routeValues, Envelope.Ok(result));
        }


        private IActionResult HandleError(Result result)
        {
            if (result.Error == Errors.General.NotFound())
            {
                return NotFound(Envelope.Error(result.Error));
            }

            if (result.Error == Errors.User.IncorrectEmailOrPassword())
            {
                return Unauthorized(Envelope.Error(result.Error));
            }

            if (result.Error == Errors.User.EmailIsTaken() || result.Error == Errors.Training.AlreadyStarted())
            {
                return Conflict(Envelope.Error(result.Error));
            }

            if( result.Error == Errors.Training.FailedToStartTraining() || result.Error == Errors.Device.DeviceUnreachable())
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            return BadRequest(Envelope.Error(result.Error));
        }
    }
}