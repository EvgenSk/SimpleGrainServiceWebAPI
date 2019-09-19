using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace SimpleGrainServiceWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleGrainServiceController : ControllerBase
    {
        public SimpleGrainServiceController(IGrainFactory grainFactory)
        {
            GrainFactory = grainFactory;
        }

        public IGrainFactory GrainFactory { get; }

        // GET: api/SimpleGrainService
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var grain = GrainFactory.GetGrain<IGrainWithSimpleService>(0);
            return new List<string> { await grain.GetStringOption() };
        }

        // GET: api/SimpleGrainService/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            var grain = GrainFactory.GetGrain<IGrainWithSimpleService>(id);
            return await grain.GetStringOption();
        }

        // POST: api/SimpleGrainService
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SimpleGrainService/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
