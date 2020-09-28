using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
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


        [ForeignKey(nameof(Radio))]
        public int RadioId { get; set; }
        public Radio Radios { get; set; }
    }
}
