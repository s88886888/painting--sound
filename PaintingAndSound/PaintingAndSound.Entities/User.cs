using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintingAndSound.Entities
{
    [Table("User")]
    public class User : BasicsBase
    {
        public User()
        {
            Works = new List<Works>();
            //WorksComments = new List<WorksComments>();
            UserTeams = new List<UserTeam>();
            Fans = new List<FansAndUser>();
            Radios = new List<Radio>();
            Paintings = new List<Painting>();
        }
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
        /// 用户作品
        /// </summary>
        public List<Works> Works { get; set; }
        /// <summary>
        /// 用户对作品的评论
        /// </summary>
        //public List<WorksComments> WorksComments { get; set; }
        /// <summary>
        /// 用户粉丝数
        /// </summary>
        public List<FansAndUser>  Fans { get; set; }
        public List<UserTeam>  UserTeams { get; set; }
        public List<Radio> Radios { get; set; }
        public List<Painting> Paintings { get; set; }
    }
}
