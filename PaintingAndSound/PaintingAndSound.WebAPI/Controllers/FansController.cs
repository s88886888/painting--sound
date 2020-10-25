using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaintingAndSound.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FansController : ControllerBase
    {
        private readonly IEntityRepository<Fans> entityRepositoryFans;
        private readonly HSDbContext hSDbContext;
        private readonly IMapper mapper;

        public FansController(IEntityRepository<Fans> entityRepositoryFans, HSDbContext hSDbContext, IMapper mapper)
        {
            this.entityRepositoryFans = entityRepositoryFans;
            this.hSDbContext = hSDbContext;
            this.mapper = mapper;
        }
        // GET: api/<FansController>
        [HttpGet]
        public async Task<IActionResult> GetFansAll()
        {
            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;
            var User = hSDbContext.FansAndUsers.Where(a => a.UserId == Convert.ToInt32(user));
            if (User == null)
            {
                return NotFound("你还没有粉丝");
            }
            return Ok(User);

        }

        // GET api/<FansController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FansController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FansController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FansController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
