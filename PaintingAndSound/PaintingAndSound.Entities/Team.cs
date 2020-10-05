using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Team")]
    public class Team:BasicsBase
    {
        public Team()
        {
            UserTeams = new List<UserTeam>();
        }
        /// <summary>
        /// 成员数量
        /// </summary>
        public int TeamNumber { get; set; }

        /// <summary>
        /// 团队简介
        /// </summary>
        public int TeamSynopsis { get; set; }
        public List<UserTeam>  UserTeams { get; set; }

    }
}
