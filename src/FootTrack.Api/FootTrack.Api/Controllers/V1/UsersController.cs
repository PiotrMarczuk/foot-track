using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FootTrack.Api.Attributes;
using FootTrack.Api.Contracts.V1;
using FootTrack.Api.Dtos;
using FootTrack.Api.Dtos.Requests;
using FootTrack.Api.Dtos.Responses;
using FootTrack.BusinessLogic.Models;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services.Interfaces;
using FootTrack.Shared.Common;

namespace FootTrack.Api.Controllers.V1
{
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

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var email = Email.Create(loginDto.Email);
            var password = Password.Create(loginDto.Password);

            Result result = Result.Combine(email, password);

            if (result.IsFailure)
            {
                // TODO
                return BadRequest();
            }

            var authenticatedUser = await _userService
                .AuthenticateAsync(new UserCredentials(email.Value, password.Value));

            if (authenticatedUser.IsFailure)
            {
                // TODO
                return BadRequest();
            }

            return Ok(_mapper.Map<AuthenticatedUserDto>(authenticatedUser.Value));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Users.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            var email = Email.Create(registerDto.Email);
            var password = Password.Create(registerDto.Password);

            if (Result.Combine(email, password).IsFailure)
            {
                // TODO
                return BadRequest();
            }

            var userResult = await _userService.RegisterAsync(new UserToBeRegistered(email.Value, registerDto.FirstName,
                registerDto.LastName, password.Value));

            if (userResult.IsFailure)
            {
                return BadRequest();
            }

            return CreatedAtAction(
                nameof(GetById),
                new {id = userResult.Value.Id},
                _mapper.Map<AuthenticatedUserDto>(userResult.Value));
        }

        [Authorize]
        [HttpGet(ApiRoutes.Users.GetById)]
        [Cached(300)]
        public async Task<IActionResult> GetById(string id)
        {
            var idResult = Id.Create(id);

            if (idResult.IsFailure)
            {
                // TODO
                return BadRequest();
            }

            var userResult = await _userService
                .GetByIdAsync(idResult.Value);

            if (userResult.IsFailure)
            {
                // TODO
                return BadRequest();
            }

            return Ok(_mapper.Map<UserDto>(userResult.Value));
        }
    }
}