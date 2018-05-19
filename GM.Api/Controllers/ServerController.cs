using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using GM.DAL.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GM.Api.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly IGenericRepository<Server> _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger _logger;

        public ServerController(IGenericRepository<Server> repository, IHttpContextAccessor accessor, ILogger logger)
        {
            _repository = repository;
            _accessor = accessor;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Server>> Get()
        {
            var ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            _logger.LogInformation(ip);

            return await _repository.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Server> Get(long id)
        {
            return await _repository.Find(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]Server model)
        {
            await _repository.Add(model);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(long id, [FromBody]Server model)
        {
            await _repository.Update(model);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

    }
}
