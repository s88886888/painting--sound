﻿using System;
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
        [HttpPost("CreateRadioAsync")]
        public async Task<IActionResult> CreateRadioAsync([FromBody] RadioViewModel radioViewModel)
        {
            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;
            radioViewModel.UserId = Convert.ToInt32(user);
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
        /// <summary>
        /// 搜素FM的一条信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetRadiosById(int id)
        {
            if (!entityRepositoryRadio.PaintingExists(id))
            {
                return NotFound("找不到该幅画");
            }
            var painting = entityRepositoryRadio.GetSingle(id);
            return Ok(painting);
        }









    }
}
