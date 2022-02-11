﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffDBContext_Code_first.Model.DTO;
using StaffWebApi.BL.Model;
using StaffWebApi.BL.Services;
using StaffWebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaffWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UsersService userService;
        private readonly SignInManager<UserDbDto> signInManager;

        public AccountController(UsersService userService, SignInManager<UserDbDto> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public class LoginRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public bool RememberMe { get; set; }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password,
                request.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public class ChangePasswordRequest
        {
            public string NewPassword { get; set; }
        }

        [HttpGet]
        public UserProfileApiDto Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var profile = new UserProfileApiDto()
            {
                UserName = identity.Name,
                Email = identity.FindFirst(ClaimTypes.Email)?.Value,
                FirstName = identity.FindFirst("FirstName")?.Value,
                MiddleName = identity.FindFirst("MiddleName")?.Value,
                LastName = identity.FindFirst("LastName")?.Value,
            };
            return profile;
        }

        [HttpGet("roles")]
        public List<string> GetRoles()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            return identity.FindAll(ClaimTypes.Role).Select(role => role.Value).ToList();
        }

        [HttpPut]
        public async Task<ActionResult> PutProfile(UserProfileApiDto profile)
        {
            if (profile.UserName != HttpContext.User.Identity.Name)
            {
                return BadRequest("Имя пользователя некорректно.");
            }

            var result = await userService.UpdateProfile(profile,true);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        [HttpPost("password")]
        public async Task<ActionResult> PostPassword([FromForm] string password)
        {
            var result = await userService.ResetPassword(HttpContext.User.Identity.Name, password);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }
    }
}
