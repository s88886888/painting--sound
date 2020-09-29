using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
   public class PaintingComment:BasicsBase
    {
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comments { get; set; }


    
        public int PaintingId { get; set; }


        public int UserId { get; set; }


    }
}
