using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.DB
{
   public class PaintingfComment:BasicsBase
    {
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comments { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User Users{ get; set; }

    }
}
