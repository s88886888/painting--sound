using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;

namespace PaintingAndSound.WebAPI.Controllers
{
    [Route("api/radio")]
    [ApiController]
    public class RadiosController : ControllerBase
    {
        private readonly IEntityRepository<Radio> entityRepositoryRadio;

        public RadiosController(IEntityRepository<Radio> entityRepositoryRadio)
        {
            this.entityRepositoryRadio = entityRepositoryRadio;
        }
        /// <summary>
        /// 获取所有音乐
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRadioAll()
        {
            var radios = await entityRepositoryRadio.GetAllAsyn();
            return Ok(radios);
        }
    }
}
