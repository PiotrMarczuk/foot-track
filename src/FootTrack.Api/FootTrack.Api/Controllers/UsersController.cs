﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FootTrack.Api.Attributes;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
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

        // POST api/users/login
        [AllowAnonymous]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel loginViewModel)
        {
            return Ok(await _userService
                .AuthenticateAsync(loginViewModel));
        }

        // POST api/users/register
        [AllowAnonymous]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel registerViewModel)
        {
            var user = await _userService
                .CreateAsync(registerViewModel);

            return CreatedAtAction(
                nameof(GetById),
                new { id = user.Id },
                _mapper.Map<UserViewModel>(user));
        }

        // GET api/users/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService
                .GetByIdAsync(id);

            return Ok(_mapper.Map<UserViewModel>(user));
        }
    }
}
