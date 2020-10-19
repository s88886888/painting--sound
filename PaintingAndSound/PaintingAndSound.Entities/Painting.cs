using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Painting")]
    public class Painting : BasicsBase
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PaintingUrl { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Works Works { get; set; }

        public List<PaintionPhotos> PaintionPhotos { get; set; }
    }
}
