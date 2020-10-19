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
    [Authorize]

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
        [HttpGet]
        public async Task<IActionResult> GetPaintingAll()
        {
            var Paintings = await entityRepositoryPainting.GetAllAsyn();
            List<PaintingViewModel> PaintingViewModel = new List<PaintingViewModel>();
            mapper.Map(Paintings, PaintingViewModel);
            return Ok(PaintingViewModel);
        }
        /// <summary>
        /// 增加一个画画
        /// </summary>
        /// <param name="paintingViewModel"></param>
        /// <returns></returns>
        [HttpPost("CreatePaintingAsync")]
        public async Task<IActionResult> CreatePaintingAsync([FromBody] PaintingViewModel paintingViewModel)
        {

            Painting painting = new Painting();
            mapper.Map(paintingViewModel, painting);
            await entityRepositoryPainting.AddOrEditAndSaveAsyn(painting);
            return Ok("OK");
        }
        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]

        public async Task<IActionResult> DeletePaintingAsync(int Id)
        {
            var Painting = await entityRepositoryPainting.GetSingleAsyn(Id);
            if (Painting == null)
            {
                return NotFound();
            }
            entityRepositoryPainting.DeleteAndSave(Painting);
            return Ok("Ok");
        }
        [HttpPatch]//局部更新
        public async Task<IActionResult> UpdatePaintingAsync([FromBody] PaintingViewModel paintingViewModel)
        {
            if (paintingViewModel == null)
            {
                return NotFound();
            }
            Painting painting = new Painting();
            mapper.Map(paintingViewModel, painting);
            await entityRepositoryPainting.AddOrEditAndSaveAsyn(painting);//如果数据库中有就增加，没有就修改
            return Ok("Ok");
        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="paintingCommentViewModels"></param>
        /// <returns></returns>

        [HttpPost("CreatePaintingCommenAsync")]
        public async Task<IActionResult> CreatePaintingCommenAsync([FromBody] PaintingCommentViewModel paintingCommentViewModels)
        {
            //前端给出具体的画Id----进行评论
            var paintions = await entityRepositoryPainting.GetSingleAsyn(a => a.Id == paintingCommentViewModels.PaintingId);
            var paintingCommentViewModel = new PaintingCommentViewModel();
            var paintingComment = new WorksComments();
            if (paintions == null)
            {
                return Ok("没有这幅画");
            }
            else
            {
                //获取当前登入tokon的Id string类型
                var user = HttpContext.AuthenticateAsync().Result.Principal.Claims.FirstOrDefault(a => a.Type.Equals("id"))?.Value;
                paintingComment.User.Id = Convert.ToInt32(user);
                paintingCommentViewModel.PaintingId = paintions.Id;
                paintingCommentViewModel.Name = paintingCommentViewModels.Name;
                paintingCommentViewModel.Comments = paintingCommentViewModels.Comments;
                paintingCommentViewModel.DateTime = paintingCommentViewModels.DateTime;
            }
            mapper.Map(paintingCommentViewModel, paintingComment);
            entityRepositoryPaintingComment.Add(paintingComment);
            await entityRepositoryPaintingComment.SaveAsyn();
            return Ok("评论成功");
        }
        /// <summary>
        /// 搜素一条画的信息            
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetPaintingById(int id)
        {
            if (!entityRepositoryPainting.PaintingExists(id))
            {
                return NotFound("找不到该幅画");
            }
            var painting = entityRepositoryPainting.GetSingle(id);

            return Ok(painting);
        }
        ///// <summary>
        ///// 搜素画的照片
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<IActionResult> GetPaintingByPhoto(int id)
        //{
        //    if (!entityRepositoryPainting.PaintingExists(id))
        //    {
        //        return NotFound("找不到该幅画");
        //    }
        //    var painting = entityRepositoryPainting.GetSingle(id);
        //    if (painting == null)
        //    {
        //        return NotFound("找不到该幅画");
        //    }



        //    return Ok(painting);
        //}
    }
}
