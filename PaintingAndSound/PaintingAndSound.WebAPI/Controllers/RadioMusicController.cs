using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RadioMusicController : ControllerBase
    {
        private readonly IEntityRepository<Radio> entityRepositoryRadio;
        private readonly IEntityRepository<RadioMusic> entityRepositoryRadioMusic;
        private readonly IMapper mapper;

        public RadioMusicController(IEntityRepository<Radio> entityRepositoryRadio, IEntityRepository<RadioMusic> entityRepositoryRadioMusic, IMapper mapper)
        {
            this.entityRepositoryRadio = entityRepositoryRadio;
            this.entityRepositoryRadioMusic = entityRepositoryRadioMusic;
            this.mapper = mapper;
        }
        // GET: api/<RadioMusicController>
        [HttpGet("GetRadioMusicAll")]
        public async Task<IActionResult> GetRadioMusicAll()
        {
            var radioMusic =await entityRepositoryRadioMusic.GetAllAsyn();
            if (radioMusic == null)
            {
                return NotFound("没有FM，请添加");
            }
            var radioMusicViewModel = mapper.Map<IEnumerable<RadioMusicViewModel>>(radioMusic);
            return Ok(radioMusicViewModel);
        }

        // GET api/<RadioMusicController>/5
        [HttpGet("{RadioMusicId}",Name =nameof(GetRadioMusicById))]
        public async Task<IActionResult>GetRadioMusicById(int RadioMusicId)
        {
            var radioMusic = await entityRepositoryRadioMusic.GetSingleAsyn(RadioMusicId);
            if (radioMusic == null)
            {
                return NotFound("没有这条FM");
            }
            var radioMusicViewModel = mapper.Map<RadioMusicViewModel>(radioMusic);
            return Ok(radioMusicViewModel);
        }

        // POST api/<RadioMusicController>
        [HttpPost]
        public async Task<IActionResult>CreateRadioMusic([FromBody] RadioMusicViewModel radioMusicViewModel)
        {
            if (radioMusicViewModel == null)
            {
                return NotFound("请重新输入");
            }
            var radioMusic = mapper.Map<RadioMusic>(radioMusicViewModel);
            entityRepositoryRadioMusic.Add(radioMusic);
            await entityRepositoryRadioMusic.SaveAsyn();
            //var paintionPhotosModels = mapper.Map<PaintionPhotosViewModel>(paintionPhotos);//应该不用返回View Model
            return CreatedAtRoute(nameof(GetRadioMusicById), new { RadioMusicId = radioMusic.Id }, radioMusic);
        }

        // PUT api/<RadioMusicController>/5
        [HttpPut("{RadioMusicId}")]
        public async Task<IActionResult> UpdateRadioMusic(int RadioMusicId, [FromBody] RadioMusicViewModel radioMusicViewModel)
        {
            var radioMusic =await entityRepositoryRadioMusic.GetSingleAsyn(RadioMusicId);
            if (radioMusic == null)
            {
                return NotFound("没有这条FM");
            }
            mapper.Map(radioMusicViewModel, radioMusic);
            await entityRepositoryRadioMusic.AddOrEditAndSaveAsyn(radioMusic);
            //var paintionPhotosModels = mapper.Map<PaintionPhotosViewModel>(radioMusic);//应该用不上
            return CreatedAtRoute(nameof(GetRadioMusicById), new { RadioMusicId = radioMusic.Id }, radioMusic);
        }

        // DELETE api/<RadioMusicController>/5
        [HttpDelete("{RadioMusicId}")]
        public async Task<IActionResult>DeleteRadioMusic(int RadioMusicId)
        {
            var radioMusic =await entityRepositoryRadioMusic.GetSingleAsyn(RadioMusicId);
            if (radioMusic == null)
            {
                return NotFound("没有这条FM");
            }
            entityRepositoryRadioMusic.DeleteAndSave(radioMusic);
            return Ok("OK");
        }
    }
}
