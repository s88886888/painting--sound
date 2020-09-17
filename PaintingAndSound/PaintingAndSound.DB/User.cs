using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingAndSound.DB
{
    public class User : BasicsBase
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        /// <summary>
        ///头像
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Synopsis { get; set; }
        public string PassWord { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCount { get; set; }
        /// <summary>
        /// 画外键Id
        /// </summary>
        /// 
        public List<Painting> Paintings { get; set; }
        public List<Radio> Radios { get; set; }

    }
}
