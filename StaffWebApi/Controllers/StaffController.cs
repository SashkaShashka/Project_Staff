using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        StaffService service;

        public StaffController(StaffService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: api/<StaffController>
        [HttpGet]
        public async Task<IEnumerable<StaffApiDto>> Get(string search ,string sortDate)
        {
            bool sortAsc = sortDate?.ToLower() == "asc";
            bool sortDesc = sortDate?.ToLower() == "desc";
            return sortAsc || sortDesc ?
                await service.GetAsync(search, sortAsc)
                : await service.GetAsync(search, null);
        }
        // GET: api/<StaffController>
        
        [Authorize(Roles = "Employee, Manager, Admin")]
        [HttpGet("MiniList")]
        public async Task<IEnumerable<MiniStaffApiDto>> MiniGet(string search, string sortDate)
        {
            bool sortAsc = sortDate?.ToLower() == "asc";
            bool sortDesc = sortDate?.ToLower() == "desc";
            return sortAsc || sortDesc ?
                await service.MiniGetAsync(search, sortAsc)
                : await service.MiniGetAsync(search, null);
        }
        [Authorize(Roles = "Manager, Admin")]
        // GET: api/<StaffController/TotalSalary>
        [HttpGet("TotalSalary")]
        public async Task<decimal> GetTotalSalary()
        {
           return await service.GetTotalSalaryAsync();
        }

        // GET api/<StaffController>/5
        [Authorize(Roles = "Employee, Manager, Admin")]
        [HttpGet("{serviceNumber}")]
        public async Task<ActionResult<StaffApiDto>> Get(int serviceNumber)
        {
            (StaffApiDto staff, Exception ex) = await service.GetAsync(serviceNumber);
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var roles = identity.FindFirst(ClaimTypes.Role)?.Value;
            if (roles == "Employee")
                if (identity.Name != staff.User)
                    return NotFound();
            if (ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(500);
            }
            return staff;
        }

        // POST api/<StaffController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StaffApiDto staff)
        {
            Exception ex = await service.CreateAsync(staff);
            if (ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(500);
            }
            return Ok();
        }

        // PUT api/<StaffController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{serviceNumber}")]
        public async Task<ActionResult> Put(int serviceNumber, [FromBody] StaffApiDto staff)
        {
            Exception ex = await service.UpdateAsync(serviceNumber, staff);
            if (ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                if (ex is ConflictIdException)
                {
                    return Conflict(ex.Message);
                }
                if (ex is AlreadyExistsException)
                {
                    return Conflict(ex.Message);
                }
                return StatusCode(500);
            }
            return Ok();
        }

        //// Put api/<StaffController>/AddPosition
        //[HttpPut("{serviceNumber}/AddPosition")]
        //public async Task<ActionResult> AddPositions(int serviceNumber, [FromBody] StaffApiDto staff)
        //{
        //    Exception ex = await service.AddPositionAsync(serviceNumber, staff);
        //    if (ex != null)
        //    {
        //        if (ex is ArgumentException)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //        if (ex is KeyNotFoundException)
        //        {
        //            return NotFound(ex.Message);
        //        }
        //        if (ex is ConflictIdException)
        //        {
        //            return Conflict(ex.Message);
        //        }
        //        if (ex is AlreadyExistsException)
        //        {
        //            return Conflict(ex.Message);
        //        }
        //        return StatusCode(500);
        //    }
        //    return Ok();
        //}

        // Put api/<StaffController>/UpdatePosition

        [Authorize(Roles = "Admin")]
        [HttpPut("{serviceNumber}/UpdatePosition")]
        public async Task<ActionResult> UpdatePosition(int serviceNumber, [FromBody] StaffPositionsApiDto staff)
        {
            Exception ex = await service.UpdatePositionAsync(serviceNumber, staff);
            if (ex != null)
            {
                if (ex is ArgumentException)
                {
                    return BadRequest(ex.Message);
                }
                if (ex is KeyNotFoundException)
                {
                    return NotFound(ex.Message);
                }
                if (ex is ConflictIdException)
                {
                    return Conflict(ex.Message);
                }
                if (ex is AlreadyExistsException)
                {
                    return Conflict(ex.Message);
                }
                return StatusCode(500);
            }
            return Ok();
        }

        //// Put api/<StaffController>/DeletePosition
        //[HttpPut("{serviceNumber}/DeletePosition")]
        //public async Task<ActionResult> DeletePositions(int serviceNumber, [FromBody] StaffApiDto staff)
        //{
        //    Exception ex = await service.DeletePositionAsync(serviceNumber, staff);
        //    if (ex != null)
        //    {
        //        if (ex is ArgumentException)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //        if (ex is KeyNotFoundException)
        //        {
        //            return NotFound(ex.Message);
        //        }
        //        if (ex is ConflictIdException)
        //        {
        //            return Conflict(ex.Message);
        //        }
        //        if (ex is AlreadyExistsException)
        //        {
        //            return Conflict(ex.Message);
        //        }
        //        return StatusCode(500);
        //    }
        //    return Ok();
        //}

        // DELETE api/<StaffController>/5

        [Authorize(Roles = "Admin")]
        [HttpDelete("{serviceNumber}")]
        public async Task<ActionResult<PositionApiDto>> Delete(int serviceNumber)
        {
            (var staff, Exception ex) = await service.DeleteAsync(serviceNumber);
            if (ex != null)
            {
                if (ex is ArgumentException)
                    return BadRequest(ex.Message);
                if (ex is KeyNotFoundException)
                    return NotFound(ex.Message);
                return StatusCode(500);
            }
            return Ok(staff);
        }
    }
}
