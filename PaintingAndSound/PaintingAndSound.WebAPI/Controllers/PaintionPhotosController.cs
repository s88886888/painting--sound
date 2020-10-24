using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaintingAndSound.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintionPhotosController : ControllerBase
    {
        private readonly IEntityRepository<Painting> entityRepositoryPainting;
        private readonly IEntityRepository<PaintionPhotos> entityRepositoryPaintionPhotos;
        private readonly IMapper mapper;

        public PaintionPhotosController(IEntityRepository<Painting> _entityRepositoryPainting,
            IEntityRepository<PaintionPhotos> _entityRepositoryPaintionPhotos,
            IMapper _mapper
            )
        {
            entityRepositoryPainting = _entityRepositoryPainting;
            entityRepositoryPaintionPhotos = _entityRepositoryPaintionPhotos;
            mapper = _mapper;
        }
        // GET: api/<PaintionPhotosController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PaintionPhotosController>/5
        [HttpGet("PaintingPhotosById")]
        public async Task<IActionResult> PaintingPhotosById(int Id)
        {
            var paintingPhoto = await entityRepositoryPaintionPhotos.GetSingleAsyn(Id);
            if (paintingPhoto == null)
            {
                return NotFound("数据异常");
            }

            var paintionPhotos = mapper.Map<PaintionPhotosViewModel>(paintingPhoto);
            return Ok(paintionPhotos);
        }

        // POST api/<PaintionPhotosController>
        [HttpPost("CreatePaintingPhotosAsync")]
        public async Task<IActionResult> CreatePaintingPhotosAsync([FromQuery] int Id, [FromBody] PaintionPhotosViewModel paintionPhotosViewModel)
        {
            if (Id == 0)
            {
                return NotFound("没有这个画集");
            }
            else if (paintionPhotosViewModel == null)
            {
                return NotFound("没有数据");
            }
            var paintionPhotos = mapper.Map<PaintionPhotos>(paintionPhotosViewModel);
            paintionPhotos.PaintingId = Id;
             entityRepositoryPaintionPhotos.Add(paintionPhotos);
            await entityRepositoryPaintionPhotos.SaveAsyn();
            var paintionPhotosModels = mapper.Map<PaintionPhotosViewModel>(paintionPhotos);
            return CreatedAtAction("PaintingPhotosById", new { paintionPhotos.Id }, paintionPhotosModels);
        }

        // PUT api/<PaintionPhotosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PaintionPhotosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
