using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Radio")]
    public class Radio:BasicsBase
    {
        public Works  Works { get; set; }
        /// <summary>
        /// 音乐文件路径
        /// </summary>
        /// 
        public string RadioUrl { get; set; }
    }
}
