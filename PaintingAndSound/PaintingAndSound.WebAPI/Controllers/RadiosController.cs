using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel;
using PaintingAndSound.WebAPI.JWT;

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
        [HttpPost]
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
        [HttpDelete]
       
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
        [HttpPatch]//局部更新
        public async Task<IActionResult> UpdateRadioAsync([FromBody] RadioViewModel radioViewModel)
        {
            if (radioViewModel == null)
            {
                return NotFound();
            }
            Radio radio = new Radio();
            mapper.Map(radioViewModel, radio);
            await entityRepositoryRadio.AddOrEditAndSaveAsyn(radio);//如果数据库中有就增加，没有就修改
            return Ok("Ok");
        }

        //public async Task<object> GetJwtStr(string name, string pass)
        //{
        //    // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
        //    TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = "Admin" };
        //    var jwtStr = JwtHelper.IssueJ
        //        wt(tokenModel);//登录，获取到一定规则的 Token 令牌
        //    var suc = true;
        //    return Ok(new
        //    {
        //        success = suc,
        //        token = jwtStr
        //    });
        //}
    }
}
