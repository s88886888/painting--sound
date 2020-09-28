using PaintingAndSound.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel.AutoMapper
{
   public class PaintingViewModel//画画ViewModel
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


        public string PaintingUrl { get; set; }
        public int UserId { get; set; }
    }
}
