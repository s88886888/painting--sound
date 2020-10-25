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
        public async Task<IActionResult> GetPaintingPhotoAll()
        {
            var painPhotos = await entityRepositoryPaintionPhotos.GetAllAsyn();
            if (painPhotos == null)
            {
                return NotFound("没有图片，请添加图片");
            }
            var retupainPhoto = mapper.Map<IEnumerable<PaintionPhotosViewModel>>(painPhotos);
            return Ok(retupainPhoto);
        }

        // GET api/<PaintionPhotosController>/5
        [HttpGet("{PaintingPhotosById}", Name = nameof(GetPaintingPhotosById))]
        public async Task<IActionResult> GetPaintingPhotosById(int PaintingPhotosById)
        {
            var paintingPhoto = await entityRepositoryPaintionPhotos.GetSingleAsyn(PaintingPhotosById);
            if (paintingPhoto == null)
            {
                return NotFound("没有这张图片");
            }

            var paintionPhotos = mapper.Map<PaintionPhotosViewModel>(paintingPhoto);
            return Ok(paintionPhotos);
        }

        // POST api/<PaintionPhotosController>
        [HttpPost("{PaintingId}")]
        public async Task<IActionResult> CreatePaintingPhotosAsync(int PaintingId, [FromBody] PaintionPhotosViewModel paintionPhotosViewModel)
        {
            var paintingPhoto = await entityRepositoryPainting.GetSingleAsyn(PaintingId);
            if (paintingPhoto == null)
            {
                return NotFound("没有这个画集");
            }
            else if (paintionPhotosViewModel == null)
            {
                return NotFound("没有数据");
            }
            var paintionPhotos = mapper.Map<PaintionPhotos>(paintionPhotosViewModel);
            paintionPhotos.PaintingId = paintingPhoto.Id;
            entityRepositoryPaintionPhotos.Add(paintionPhotos);
            await entityRepositoryPaintionPhotos.SaveAsyn();
            var paintionPhotosModels = mapper.Map<PaintionPhotosViewModel>(paintionPhotos);
            return CreatedAtRoute(nameof(GetPaintingPhotosById), new { PaintingPhotosById = paintionPhotos.Id }, paintionPhotosModels);
        }

        // PUT api/<PaintionPhotosController>/5
        [HttpPut("{paintingPhotoId}")]
        public async Task<IActionResult> UpdatePaintingPhoto(int paintingPhotoId, PaintionPhotosViewModel paintionPhotosViewModel)
        {
            var paintingPhoto = entityRepositoryPaintionPhotos.GetSingle(paintingPhotoId);
            if (paintingPhoto == null)
            {
                return NotFound("没有这张图片");
            }
            mapper.Map(paintionPhotosViewModel, paintingPhoto);
            await entityRepositoryPaintionPhotos.AddOrEditAndSaveAsyn(paintingPhoto);
            var paintionPhotosModels = mapper.Map<PaintionPhotosViewModel>(paintingPhoto);

            return CreatedAtRoute(nameof(GetPaintingPhotosById), new { PaintingPhotosById = paintingPhoto.Id }, paintionPhotosModels);
        }

        // DELETE api/<PaintionPhotosController>/5
        [HttpDelete("{paintingPhotoId}")]
        public async Task<IActionResult> DeletePaintingPhoto(int paintingPhotoId)
        {
            var paintingPhoto = await entityRepositoryPaintionPhotos.GetSingleAsyn(paintingPhotoId);
            if (paintingPhoto == null)
            {
                return NotFound("没有这张图片");
            }
            entityRepositoryPaintionPhotos.DeleteAndSave(paintingPhoto);
            return Ok("OK");
        }
    }
}
