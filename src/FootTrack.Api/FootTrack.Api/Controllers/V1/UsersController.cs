using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FootTrack.Api.Attributes;
using FootTrack.Api.Contracts.V1;
using FootTrack.Api.Dtos.Requests;
using FootTrack.Api.Dtos.Responses;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Shared;

namespace FootTrack.Api.Controllers.V1
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IMapper mapper, IUserService userService) : base(mapper)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            UserCredentials userCredentials = UserCredentials.Create(loginDto.Email, loginDto.Password).Value;

            Result<AuthenticatedUser> authenticatedUserResult = await _userService
                .AuthenticateAsync(userCredentials);

            return OkOrError<AuthenticatedUserDto, AuthenticatedUser>(authenticatedUserResult);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Users.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
        {
            UserToBeRegistered userToBeRegistered = UserToBeRegistered.Create(
                    registerDto.Email,
                    registerDto.FirstName,
                    registerDto.LastName,
                    registerDto.Password)
                .Value;

            Result<AuthenticatedUser> userResult = await _userService.RegisterAsync(userToBeRegistered);

            return CreatedAtOrError<AuthenticatedUserDto, AuthenticatedUser>(
                userResult,
                nameof(GetById),
                new
                {
                    id = userResult.IsSuccess
                        ? userResult.Value.Id.Value
                        : default,
                });
        }

        [HttpGet(ApiRoutes.Users.GetById)]
        [Cached(300)]
        public async Task<IActionResult> GetById(string id)
        {
            Result<Id> idResult = Id.Create(id);

            if (idResult.IsFailure)
            {
                return Error(idResult);
            }

            Result<UserData> userResult = await _userService
                .GetByIdAsync(idResult.Value);

            return OkOrError<UserDto, UserData>(userResult);
        }
    }
}