using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintingAndSound.API.ViewModel
{
    public class RadioViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
        //是否删除
        public bool IsDelete { get; set; } = false;
        public string RadioUrl { get; set; }
    }
}
