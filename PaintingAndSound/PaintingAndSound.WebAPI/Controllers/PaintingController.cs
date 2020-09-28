using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IEntityRepository<PaintingComment> entityRepositoryPaintingComment;

        private readonly IMapper mapper;

        public PaintingController(IEntityRepository<Painting> _entityRepositoryPainting, IMapper _mapper, IEntityRepository<PaintingComment> _entityRepositoryPaintingComment)
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


        [HttpPost("CreatePaintingCommenAsync")]
        public async Task<IActionResult> CreatePaintingCommenAsync([FromBody] PaintingCommentViewModel paintingCommentViewModels)
        {

            var paintions = entityRepositoryPainting.GetSingleAsyn(a => a.Id == paintingCommentViewModels.PaintingId);

            //获取当前登入的Id
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (paintions == null)
            {
                return Ok("没有这条评论");
            }



            PaintingComment paintingComment = new PaintingComment();
            PaintingCommentViewModel paintingCommentViewModel = new PaintingCommentViewModel();
            //paintingComment.PaintingId = paintingViewModel.Id;
            //paintingComment.UserId = Convert.ToInt32(user);
            mapper.Map(paintingCommentViewModel, paintingComment);
            entityRepositoryPaintingComment.Add(paintingComment);
            await entityRepositoryPaintingComment.SaveAsyn();
            return Ok("OK");
        }





    }
}
