using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
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
            if (radios == null)
            {
                return NotFound("没有FM集，请前往添加");
            }
            var radioViewModel = mapper.Map<IEnumerable<RadioViewModel>>(radios);

            //List<RadioViewModel> radioViewModel = new List<RadioViewModel>();
            //mapper.Map(radios, radioViewModel);
            return Ok(radioViewModel);
        }
        /// <summary>
        /// 搜素FM的一条信息
        /// </summary>
        [HttpGet("{RadioId}")]
        public async Task<IActionResult> GetRadiosById(int RadioId)
        {
            var radio =await entityRepositoryRadio.GetSingleAsyn(RadioId);
            if (radio == null)
            {
                return NotFound("没有这部FM集");
            }
            var radioViewModel = mapper.Map<RadioViewModel>(radio);
            return Ok(radioViewModel);
        }
        /// <summary>
        /// 增加一个音乐
        /// </summary>
        /// <param name="radioViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreateRadioAsync")]
        public async Task<IActionResult> CreateRadioAsync([FromBody] RadioViewModel radioViewModel)
        {
            if (radioViewModel == null)
            {
                return NotFound("请从新输入");
            }
            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;
            var entityRadio = mapper.Map<Radio>(radioViewModel);
            entityRadio.UserId = Convert.ToInt32(user);

            entityRepositoryRadio.Add(entityRadio);
            await entityRepositoryRadio.SaveAsyn();
            return CreatedAtRoute(nameof(GetRadiosById), new { RadioId = entityRadio.Id }, entityRadio);
        }
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{RadioId}")]
        public async Task<IActionResult> DeleteRadioAsync(int RadioId)
        {
            var radio = await entityRepositoryRadio.GetSingleAsyn(RadioId);
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
        /// <returns></returns>
        [HttpPatch("{RadioId}")]
        public async Task<IActionResult> UpdateRadioAsync(int RadioId, [FromBody]RadioViewModel radioViewModel)
        {
            var radio = await entityRepositoryRadio.GetSingleAsyn(RadioId);
            if (radio == null)
            {
                return NotFound("没有这部FM集");
            }
            mapper.Map(radioViewModel, radio);
            await entityRepositoryRadio.AddOrEditAndSaveAsyn(radio);
            //var paintionPhotosModels = mapper.Map<PaintionPhotosViewModel>(radio);//应该不用
            return CreatedAtRoute(nameof(GetRadiosById), new { RadioId = radio.Id }, radio);
        }
        










    }
}
