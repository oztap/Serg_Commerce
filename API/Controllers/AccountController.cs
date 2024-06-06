using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseAPIController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
        ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new APIResponse(401));

            var results = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!results.Succeeded) return Unauthorized(new APIResponse(401));

            return new UserDto
            {
                Email = user.Email,
                Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJvYkB0ZXN0LmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJuYmYiOjE1ODA1NTcxMDEsImV4cCI6MTU4MTE2MTkwMSwiaWF0IjoxNTE2MjM5MDIyLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEifQ.gk0lwMJRTjgLQN00eZpRiqjkCdEhbcvx4w73Hgz7Tq0OQo4npw4Qq512WSF1tQGwBcVBdtXk9I_H83AyhhBReQ",
                DisplayName = user.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {

            if (CheckEmailExistAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new APIValidationErrorResponse{Errors = new []
                {"Email address is in use"}});
            }

            
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new APIResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJvYkB0ZXN0LmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJuYmYiOjE1ODA1NTcxMDEsImV4cCI6MTU4MTE2MTkwMSwiaWF0IjoxNTE2MjM5MDIyLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEifQ.gk0lwMJRTjgLQN00eZpRiqjkCdEhbcvx4w73Hgz7Tq0OQo4npw4Qq512WSF1tQGwBcVBdtXk9I_H83AyhhBReQ",
                Email = user.Email
            };
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            return new UserDto
            {
                Email = user.Email,
                Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJvYkB0ZXN0LmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJuYmYiOjE1ODA1NTcxMDEsImV4cCI6MTU4MTE2MTkwMSwiaWF0IjoxNTE2MjM5MDIyLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUwMDEifQ.gk0lwMJRTjgLQN00eZpRiqjkCdEhbcvx4w73Hgz7Tq0OQo4npw4Qq512WSF1tQGwBcVBdtXk9I_H83AyhhBReQ",
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimPrincipleWithAddressAsync(User);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimPrincipleWithAddressAsync(HttpContext.User);
            user.Address=_mapper.Map<AddressDto,Address>(address);
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded) return Ok(_mapper.Map<Address,AddressDto>(user.Address));
            return BadRequest("Problem updating user");
        }


    }
}