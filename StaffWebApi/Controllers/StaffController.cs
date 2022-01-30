using Microsoft.AspNetCore.Mvc;
using StaffWebApi.BL.Model;
using StaffWebApi.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET api/<StaffController>/5
        [HttpGet("{serviceNumber}")]
        public async Task<ActionResult<StaffApiDto>> Get(int serviceNumber)
        {
            (StaffApiDto product, Exception ex) = await service.GetAsync(serviceNumber);
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
            return product;
        }

        // POST api/<StaffController>
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StaffController>/5
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
