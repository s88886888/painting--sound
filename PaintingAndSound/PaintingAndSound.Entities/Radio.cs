using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
  public  class Radio:BasicsBase
    {
        /// <summary>
        /// 音乐文件路径
        /// </summary>
        /// 
        public string RadioUrl { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User Users { get; set; }
    }
}
