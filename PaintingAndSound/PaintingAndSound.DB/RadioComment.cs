using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.DB
{
    /// <summary>
    /// 收音机评论
    /// </summary>
  public  class RadioComment:BasicsBase
    {
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comments { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
