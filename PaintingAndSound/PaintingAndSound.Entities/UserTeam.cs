using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("UserTeam")]
    public class UserTeam
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public User  User { get; set; }
        public Team  Team { get; set; }
    }

}
