using PaintingAndSound.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.ViewModel.AutoMapper
{
  public  class PaintingCommentViewModel//画画评论ViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        //是否删除
        public bool IsDelete { get; set; } = false;
        public string Comments { get; set; }

        public int PaintingId { get; set; }
        public int UserId { get; set; }


    }
}
