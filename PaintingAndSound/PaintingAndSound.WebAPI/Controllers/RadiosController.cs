using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel;

namespace PaintingAndSound.WebAPI.Controllers
{
    //[Produces("application/josn")]
    [Route("api/radio")]
    [ApiController]
    [Authorize]
    public class RadiosController : ControllerBase
    {
        private readonly IEntityRepository<Radio> entityRepositoryRadio;
        private readonly IMapper mapper;

        public RadiosController(IEntityRepository<Radio> entityRepositoryRadio, IMapper mapper)
        {
            this.entityRepositoryRadio = entityRepositoryRadio;
            this.mapper = mapper;
        }
        /// <summary>
        /// 获取所有音乐
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRadioAll()
        {
            var radios = await entityRepositoryRadio.GetAllAsyn();
            List<RadioViewModel> radioViewModel = new List<RadioViewModel>();
            mapper.Map(radios, radioViewModel);
            return Ok(radioViewModel);
        }
        /// <summary>
        /// 增加一个音乐
        /// </summary>
        /// <param name="radioViewModel"></param>
        /// <returns></returns>
        [HttpPost("{radioViewModel}")]
        public async Task<IActionResult> CreateRadioAsync([FromBody] RadioViewModel radioViewModel)
        {

            Radio radio = new Radio();
            mapper.Map(radioViewModel, radio);
            await entityRepositoryRadio.AddOrEditAndSaveAsyn(radio);
            return Ok("OK");
        }
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRadioAsync(int Id)
        {
            var radio = await entityRepositoryRadio.GetSingleAsyn(Id);
            if (radio == null)
            {
                return NotFound();
            }
            entityRepositoryRadio.DeleteAndSave(radio);
            return Ok("Ok");
        }
        /// <summary>
        /// 局部更新
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateRadioAsync(int Id, JsonPatchDocument<RadioViewModel> patchDocument)
        {
            var radio = await entityRepositoryRadio.GetSingleAsyn(Id);
            if (radio == null)
            {
                return NotFound();
            }
            var dtoToPatch = mapper.Map<RadioViewModel>(radio);
            patchDocument.ApplyTo(dtoToPatch);

            mapper.Map(dtoToPatch, radio);
            entityRepositoryRadio.Edit(radio);
            await entityRepositoryRadio.SaveAsyn();

            return NoContent();
        }









    }
}
