using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using FootTrack.Api.Attributes;
using FootTrack.Api.Contracts.V1;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Controllers.V1
{
    /// <summary>
    /// Operations about user.
    /// </summary>
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(
            IMapper mapper, 
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginViewModel">Model with email and password.</param>
        /// <returns>A logged in user with token.</returns>
        /// <response code="200">Returns logged in user with token.</response>
        /// <response code="400">If loginViewModel is not valid.</response>
        /// <response code="401">When credentials are wrong.</response>
        [AllowAnonymous]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [ProducesResponseType(typeof(AuthenticatedUserViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel loginViewModel)
        {
            return Ok(await _userService
                .AuthenticateAsync(loginViewModel));
        }

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="registerViewModel">Model for creating new user.</param>
        /// <returns>Newly created user.</returns>
        /// <response code="201">Returns newly created user.</response>
        /// <response code="400">If registerViewModel is not valid.</response>
        /// <response code="409">If user with provided email already exists.</response>
        [AllowAnonymous]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [ProducesResponseType(typeof(UserViewModel),StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost(ApiRoutes.Users.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel registerViewModel)
        {
            var user = await _userService
                .CreateAsync(registerViewModel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = user.Id },
                _mapper.Map<UserViewModel>(user));
        }

        /// <summary>
        /// Method for getting user data by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User data.</returns>
        /// <response code="200">Returns user data.</response>
        /// <response code="400">If provided id is not valid.</response>
        /// <response code="401">If there was no auth token provided.</response>
        /// <response code="404">If user with provided id was not found.</response>
        [Authorize]
        [ProducesResponseType(typeof(UserViewModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [HttpGet(ApiRoutes.Users.GetById)]
        [Cached(300)]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService
                .GetByIdAsync(id);

            return Ok(_mapper.Map<UserViewModel>(user));
        }
    }
}
