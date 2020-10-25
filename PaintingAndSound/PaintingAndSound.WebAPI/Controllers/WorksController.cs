using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel;

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
        private readonly IEntityRepository<PaintionPhotos> entityRepositoryPaintionPhotos;
        private readonly IMapper mapper;

        public WorksController(
            IEntityRepository<Works> _entityRepositoryWorks,
            IEntityRepository<WorksComments> _entityRepositoryWorksComment,
            IEntityRepository<Painting> _entityRepositoryPainting,
            IEntityRepository<Radio> _entityRepositoryRadio,
            IEntityRepository<PaintionPhotos> _entityRepositoryPaintionPhotos,
        IMapper _mapper
            )
        {
            entityRepositoryWorks = _entityRepositoryWorks;
            entityRepositoryWorksComment = _entityRepositoryWorksComment;
            entityRepositoryPainting = _entityRepositoryPainting;
            entityRepositoryRadio = _entityRepositoryRadio;
            entityRepositoryPaintionPhotos = _entityRepositoryPaintionPhotos;
            mapper = _mapper;
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
        [HttpGet("{id}", Name = nameof(GetWorksById))]
        public async Task<IActionResult> GetWorksById(int id)
        {
            if (!entityRepositoryWorks.WorksExists(id))
            {
                return NotFound("没有这条作品");
            }

            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;
            if (user == null)
            {
                return NotFound("请重新登陆");
            }
            var userWoks =await entityRepositoryWorks.GetSingleAsyn(a=>a.UserId== Convert.ToInt32(user) && a.Id == id);
            if (userWoks == null)
            {
                return NotFound("你还没有作品");
            }
            var WorksViewModel = mapper.Map<WorkViewModel>(userWoks);

            foreach(var i in entityRepositoryRadio.GetAll())
            {
                if (userWoks.Radios == i)
                {
                    WorksViewModel.Radios = i;
                }
            }
            foreach(var i in entityRepositoryPainting.GetAll())
            {
                if (userWoks.Paintings == i)
                {
                    WorksViewModel.Paintings = i;
                }
            }
            return Ok(WorksViewModel);

        }

        [HttpPost("AddWorks")]
        public async Task<IActionResult> AddWorks(int radioId, int PaintingId, [FromBody] CreateWoksViewModel workViewModel)
        {
            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;


            if (!entityRepositoryPainting.PaintingExistsByUserId(Convert.ToInt32(user)))
            {
                return NotFound("您没有创建过画集");
            }
            else if (!entityRepositoryRadio.RadiosExistsByUserId(Convert.ToInt32(user)))
            {

                return NotFound("您没有创建过FM音频");
            }
            //判断有没有音频
            else if (await entityRepositoryRadio.GetSingleAsyn(radioId) == null)
            {
                return NotFound("没有这条FM音频");
            }
            else if (await entityRepositoryPainting.GetSingleAsyn(PaintingId) == null)
            {
                return NotFound("没有这个画集");
            }
            var painting = await entityRepositoryPainting.GetSingleAsyn(PaintingId);
            var radio = await entityRepositoryRadio.GetSingleAsyn(radioId);
            var woks = mapper.Map<Works>(workViewModel);
            woks.Paintings = painting;
            woks.Radios = radio;
            woks.UserId = Convert.ToInt32(user);
            entityRepositoryWorks.AddAndSave(woks);
            return CreatedAtRoute(nameof(GetWorksById), new { id = woks.Id }, woks);
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
