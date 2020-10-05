using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Works")]
    public class Works:BasicsBase
    {
        public Works()
        {
            Paintings = new List<Painting>();
            //WorksComments = new List<WorksComments>();
        }

        public int RadioId { get; set; }
        public Radio  Radio { get; set; }
        public int UserId { get; set; }
        public User  User { get; set; }
        /// <summary>
        /// 作品图片
        /// </summary>
        public List<Painting>  Paintings { get; set; }
        /// <summary>
        /// 作品评论
        /// </summary>
        //public List<WorksComments> WorksComments { get; set; }

    }
}
