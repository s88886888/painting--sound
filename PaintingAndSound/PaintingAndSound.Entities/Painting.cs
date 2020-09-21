using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
   public class Painting:BasicsBase
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PaintingUrl { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User Users { get; set; }
        /// <summary>
        /// 评论
        /// </summary>
        [ForeignKey(nameof(PaintingfComment))]
        public int PaintingCommentId { get; set; }
    }
}
