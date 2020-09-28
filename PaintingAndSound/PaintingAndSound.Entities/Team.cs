using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
   public class Team:BasicsBase
    {
        /// <summary>
        /// 成员数量
        /// </summary>
        public int TeamNumber { get; set; }

        /// <summary>
        /// 团队简介
        /// </summary>
        public int TeamSynopsis { get; set; }


        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public List<User> Users { get; set; }
    }
}
