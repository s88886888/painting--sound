using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel
{
   public class RadioMusicViewModel
    {
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        //是否删除
        public bool IsDelete { get; set; } 
        public string RadioMusicUrl { get; set; }
        public string Detailed { get; set; }
        public string Image { get; set; }
    }
}
