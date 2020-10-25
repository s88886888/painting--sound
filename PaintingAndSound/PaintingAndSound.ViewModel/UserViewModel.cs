using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel
{
   public class UserViewModel
    {
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        //是否删除
        public bool IsDelete { get; set; };
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
    }
}
