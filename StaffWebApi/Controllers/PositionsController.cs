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
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        PositionsService service;

        public PositionsController(PositionsService service)
        {
            this.service = service;
        }
        // GET: api/PositionController
        [Authorize(Roles = "Employee, Manager, Admin")]
        [HttpGet]
        public async Task<IEnumerable<PositionApiDto>> Get(string search,string filterDivision)
        {
            return await service.GetAsync(search, filterDivision);
        }

        // GET api/<PositionController>/5
        [Authorize(Roles = "Employee, Manager, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionApiDto>> Get(int id)
        {
            (PositionApiDto product, Exception ex) = await service.GetAsync(id);
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

        // POST api/<PositionController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PositionApiDto product)
        {
            Exception ex = await service.CreateAsync(product);
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

        // PUT api/<PositionController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PositionApiDto position)
        {
            Exception ex = await service.UpdateAsync(id, position);
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
                return StatusCode(500);
            }
            return Ok();
        }

        // DELETE api/<PositionController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PositionApiDto>> Delete(int id)
        {
            (var position, Exception ex) = await service.DeleteAsync(id);
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
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
            return Ok(position);
        }
    }
}
