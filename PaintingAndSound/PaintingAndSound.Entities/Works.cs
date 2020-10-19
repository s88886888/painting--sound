using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Works")]
    public class Works : BasicsBase
    {
        public Works()
        {
            Paintings = new List<Painting>();
            //WorksComments = new List<WorksComments>();
            Radios = new List<Radio>();
        }

        public int RadioId { get; set; }
        public List<Radio> Radios { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        /// <summary>
        /// 作品图片
        /// </summary>
        public int PaintingId { get; set; }
        public List<Painting> Paintings { get; set; }
        /// <summary>
        /// 作品评论
        /// </summary>
        //public List<WorksComments> WorksComments { get; set; }

    }
}
