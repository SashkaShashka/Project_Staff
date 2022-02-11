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
        private readonly PositionsService service;

        public PositionsController(PositionsService service)
        {
            this.service = service;
        }
        // GET: api/PositionController
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet]
        public async Task<IEnumerable<PositionApiDto>> Get(string search, string filterDivision)
        {
            return await service.GetAsync(search, filterDivision);
        }
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet("Divisions")]
        public async Task<IEnumerable<string>> GetDivisions()
        {
            return await service.GetDivisionsAsync();
        }

        // GET api/<PositionController>/5
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionApiDto>> Get(int id)
        {
            (PositionApiDto product, Exception ex) = await service.GetAsync(id);
            var resEx = CheckException.CheckError(ex);
            if (resEx.Item1 == 200)
                return product;
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        // POST api/<PositionController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PositionApiDto product)
        {
            Exception ex = await service.CreateAsync(product);
            var resEx = CheckException.CheckError(ex);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        // PUT api/<PositionController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PositionApiDto position)
        {
            Exception ex = await service.UpdateAsync(id, position);
            var resEx = CheckException.CheckError(ex);
            if (resEx.Item1 == 200)
                return Ok();
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }

        // DELETE api/<PositionController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PositionApiDto>> Delete(int id)
        {
            (var position, Exception ex) = await service.DeleteAsync(id);
            var resEx = CheckException.CheckError(ex);
            if (resEx.Item1 == 200)
                return position;
            else
                return StatusCode(resEx.Item1, resEx.Item2);
        }
    }
}
