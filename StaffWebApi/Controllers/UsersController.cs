using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaffWebApi.BL.Model;
using StaffWebApi.BL.Services;
using StaffWebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaffWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService userService;

        public UsersController(UsersService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserProfileApiDto>> Get()
        {
            return await userService.GetProfiles();
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<UserProfileApiDto>> Get(string userName)
        {
            var profile = await userService.GetProfile(userName);
            if (profile == null)
            {
                return NotFound();
            }
            return profile;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserProfileCreateApiDto profile)
        {
            var result = await userService.Create(profile);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserProfileCreateApiDto profile)
        {
            var result = await userService.UpdateProfile(profile);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> Delete(string userName)
        {
            var result = await userService.Delete(userName);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        [HttpPost("{userName}")]
        public async Task<ActionResult> PostRole([FromRoute] string userName, [FromQuery] string roles)
        {
            var result = await userService.SetRoles(userName, roles.Split(','), HttpContext.User.Identity.Name == userName);

            return result.GetResultObject($"{result?.Message}. {result?.InnerException?.Message}");
        }

        [HttpDelete("{username}/role/{role}")]
        public async Task<ActionResult> DeleteRole(string userName, string role)
        {
            var result = await userService.RemoveFromRole(userName, role);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }
        [HttpPost("{username}/password")]
        public async Task<ActionResult> PostPassword(string userName,[FromForm] string password)
        {
            var result = await userService.ResetPassword(userName, password);
            var resEx = CheckException.CheckError(result);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }
        
    }
}
