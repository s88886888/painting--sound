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
using PaintingAndSound.UserAndRole;
using PaintingAndSound.ViewModel;
using PaintingAndSound.WebAPI.Dto;

namespace PaintingAndSound.WebAPI.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IEntityRepository<User> entityRepositoryUser;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IEntityRepository<User> entityRepositoryUser,IMapper mapper)
        {
            this.userService = userService;
            this.entityRepositoryUser = entityRepositoryUser;
            this.mapper = mapper;
        }

        /// <summary>
        /// 所有用户
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<BaseDto<IEnumerable<User>>>> Get()
        {
            var users = await userService.GetUserAsync();
            BaseDto<IEnumerable<User>> dto = new BaseDto<IEnumerable<User>>(Dto.StatusCode.Success, "", users);
            return Ok(dto);
        }

        ///// <summary>
        ///// 当前用户
        ///// </summary>
        ///// <returns></returns>
        //[Route("me")]
        //[HttpGet]
        //public async Task<ActionResult<BaseDto<User>>> UserInfo()
        //{
        //    string id = User.FindFirst("id")?.Value;
        //    var user = await userService.GetUserAsync();
        //    BaseDto<User> dto = new BaseDto<User>(Dto.StatusCode.Success, "", user);
        //    return Ok(dto);
        //}

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<BaseDto<User>>> Get(int id)
        {
            var user = await userService.GetUserAsync(id);
            BaseDto<User> dto = new BaseDto<User>(Dto.StatusCode.Success, "", user);
            return Ok(dto);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="loginParameter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BaseDto<User>>> Add(LoginParameter loginParameter)
        {
            var user = await userService.AddUserAsync(loginParameter.UserName, loginParameter.Password);
            BaseDto<User> dto = new BaseDto<User>(Dto.StatusCode.Success, "", user);
            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult>CreateUser(UserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return NotFound("请从新输入");
            }
            var user = mapper.Map<User>(userViewModel);
            entityRepositoryUser.Add(user);
            await entityRepositoryUser.SaveAsyn();
            return Ok("创建成功");
        }
    }

    public class LoginParameter
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
