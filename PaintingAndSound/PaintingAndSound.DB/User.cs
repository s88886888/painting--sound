using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingAndSound.DB
{
   public class User: BasicsBase
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        ///头像
        /// </summary>
        public string  Photo { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Synopsis { get; set; }
        public string PassWodr { get; set; }
        /// <summary>
        /// 画外键Id
        /// </summary>
        [ForeignKey(nameof(Painting))]
        public int PaintingId { get; set; }
        public List<Painting> Paintings{ get; set; }


        public int RadioId { get; set; }
        public List<Radio> Radios { get; set; }

        [ForeignKey(nameof(Fans))]
        public int FansId { get; set; }
        public List<Fans> Fanss { get; set; }


    }
}
