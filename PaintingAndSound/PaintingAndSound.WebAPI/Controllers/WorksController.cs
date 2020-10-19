using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel;
using PaintingAndSound.ViewModel.AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaintingAndSound.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {

        private readonly IEntityRepository<Works> entityRepositoryWorks;
        private readonly IEntityRepository<WorksComments> entityRepositoryWorksComment;
        private readonly IEntityRepository<Painting> entityRepositoryPainting;
        private readonly IEntityRepository<Radio> entityRepositoryRadio;
        private readonly IMapper mapper;

        public WorksController(
            IEntityRepository<Works> _entityRepositoryWorks,
            IEntityRepository<WorksComments> _entityRepositoryWorksComment,
            IEntityRepository<Painting> _entityRepositoryPainting,
            IEntityRepository<Radio> _entityRepositoryRadio
            )
        {
            entityRepositoryWorks = _entityRepositoryWorks;
            entityRepositoryWorksComment = _entityRepositoryWorksComment;
            entityRepositoryPainting = _entityRepositoryPainting;
            entityRepositoryRadio = _entityRepositoryRadio;

        }
        /// <summary>
        /// 获取全部作品的信息
        /// </summary>
        /// <returns></returns>
        // GET: api/<WorksController>
        [HttpGet]
        public async Task<IActionResult> GetWorksAll()
        {
            var Works = await entityRepositoryWorks.GetAllAsyn();
            var WorksViewModel = new List<WorkViewModel>();
            mapper.Map(Works, WorksViewModel);
            return Ok(WorksViewModel);
        }
        /// <summary>
        /// 查询一条作品的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<WorksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorksById(int id)
        {
            if (!entityRepositoryWorks.WorksExists(id))
            {
                return NotFound("没有这条作品");
            }
            var Works = await entityRepositoryWorks.GetSingleAsyn(id);
            var WorksViewModel = new List<WorksComments>();
            mapper.Map(Works, WorksViewModel);
            return Ok(WorksViewModel);

        }
        /// <summary>
        /// 创建作品
        /// </summary>
        /// <returns></returns>
        // POST api/<WorksController>
        [HttpPost("AddWorks")]
        public async Task<IActionResult> AddWorks([FromBody] WorkViewModel workViewModel)
        {

            var Works = new Works();
            mapper.Map(Works, workViewModel);
            return Ok("添加成功");
        }

        // PUT api/<WorksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WorksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
