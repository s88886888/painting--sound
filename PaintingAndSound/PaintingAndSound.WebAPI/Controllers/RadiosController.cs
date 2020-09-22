using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.WebAPI.JWT;

namespace PaintingAndSound.WebAPI.Controllers
{
    //[Produces("application/josn")]
    [Route("api/radio")]
    [ApiController]
    [Authorize(Roles = "admin")]


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
