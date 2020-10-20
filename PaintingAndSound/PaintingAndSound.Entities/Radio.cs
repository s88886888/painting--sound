using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Radio")]
    public class Radio:BasicsBase
    {
        
        public int UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 音乐文件路径
        /// </summary>
        /// 
        public string RadioUrl { get; set; }
        /// <summary>
        /// Radio封面
        /// </summary>
        public string Iamge { get; set; }
        public List<RadioMusic>  RadioMusics { get; set; }
    }
}
