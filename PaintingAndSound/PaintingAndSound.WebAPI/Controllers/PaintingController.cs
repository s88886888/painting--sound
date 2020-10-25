using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintingAndSound.DataAccess.Services;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel.AutoMapper;

namespace PaintingAndSound.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class PaintingController : ControllerBase
    {
        private readonly IEntityRepository<Painting> entityRepositoryPainting;
        private readonly IEntityRepository<WorksComments> entityRepositoryPaintingComment;

        private readonly IMapper mapper;

        public PaintingController(IEntityRepository<Painting> _entityRepositoryPainting, IMapper _mapper, IEntityRepository<WorksComments> _entityRepositoryPaintingComment)
        {
            entityRepositoryPainting = _entityRepositoryPainting;
            entityRepositoryPaintingComment = _entityRepositoryPaintingComment;
            mapper = _mapper;
        }



        /// <summary>
        /// 获取所有的画画
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPaintingAll")]
        public async Task<IActionResult> GetPaintingAll()
        {
            var Paintings = await entityRepositoryPainting.GetAllAsyn();
            if (Paintings == null)
            {
                return NotFound();
            }
            var PaintingsViewModel = mapper.Map<IEnumerable<PaintingViewModel>>(Paintings);
            //List<PaintingViewModel> PaintingViewModel = new List<PaintingViewModel>();
            //mapper.Map(Paintings, PaintingViewModel);
            return Ok(PaintingsViewModel);
        }
        /// <summary>
        /// 增加一个画集
        /// </summary>
        /// <param name="paintingViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreatePaintingAsync")]
        public async Task<IActionResult> CreatePaintingAsync([FromBody] PaintingViewModel paintingViewModel)
        {

            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;

            var Painting = mapper.Map<Painting>(paintingViewModel);
            Painting.UserId = Convert.ToInt32(user);
            entityRepositoryPainting.Add(Painting);
            await entityRepositoryPainting.SaveAsyn();
            return CreatedAtRoute(nameof(GetPaintingById), new { PaintingById = Painting.Id }, Painting);

        }
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{PaintingId}")]
        public async Task<IActionResult> DeletePaintingAsync(int PaintingId)
        {
            var Painting = await entityRepositoryPainting.GetSingleAsyn(PaintingId);
            if (Painting == null)
            {
                return NotFound();
            }
            entityRepositoryPainting.DeleteAndSave(Painting);
            return Ok("Ok");
        }
        [HttpPut("{PaintingId}")]//局部更新
        public async Task<IActionResult> UpdatePaintingAsync(int PaintingId, [FromBody] PaintingViewModel paintingViewModel)
        {
            if (paintingViewModel == null)
            {
                return NotFound();
            }
            var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;

            var Painting = await entityRepositoryPainting.GetSingleAsyn(PaintingId);
            if (Painting == null)
            {
                return NotFound("没有这个画集");
            }
            Painting.UserId = Convert.ToInt32(user);
            mapper.Map(paintingViewModel, Painting);
            await entityRepositoryPainting.AddOrEditAndSaveAsyn(Painting);
            //var paintionPhotosModels = mapper.Map<PaintingViewModel>(Painting);

            return CreatedAtRoute(nameof(GetPaintingById), new { PaintingById = Painting.Id }, Painting);

        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="paintingCommentViewModels"></param>
        /// <returns></returns>

        //[HttpPost("CreatePaintingCommenAsync")]
        //public async Task<IActionResult> CreatePaintingCommenAsync([FromBody] PaintingCommentViewModel paintingCommentViewModels)
        //{
        //    //前端给出具体的画Id----进行评论
        //    var paintions = await entityRepositoryPainting.GetSingleAsyn(a => a.Id == paintingCommentViewModels.PaintingId);
        //    var paintingCommentViewModel = new PaintingCommentViewModel();
        //    var paintingComment = new WorksComments();
        //    if (paintions == null)
        //    {
        //        return Ok("没有这幅画");
        //    }
        //    else
        //    {
        //        //获取当前登入tokon的Id string类型
        //        var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;
        //        paintingComment.User.Id = Convert.ToInt32(user);
        //        paintingCommentViewModel.PaintingId = paintions.Id;
        //        paintingCommentViewModel.Name = paintingCommentViewModels.Name;
        //        paintingCommentViewModel.Comments = paintingCommentViewModels.Comments;
        //        paintingCommentViewModel.DateTime = paintingCommentViewModels.DateTime;
        //    }
        //    mapper.Map(paintingCommentViewModel, paintingComment);
        //    entityRepositoryPaintingComment.Add(paintingComment);
        //    await entityRepositoryPaintingComment.SaveAsyn();
        //    return Ok("评论成功");
        //}
        /// <summary>
        /// 搜素一条画的信息            
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{PaintingById}", Name = nameof(GetPaintingById))]
        public async Task<IActionResult> GetPaintingById(int PaintingById)
        {
            if (!entityRepositoryPainting.PaintingExists(PaintingById))
            {
                return NotFound("找不到该幅画");
            }
            var painting = await entityRepositoryPainting.GetSingleAsyn(PaintingById);

            return Ok(painting);
        }
    }
}
