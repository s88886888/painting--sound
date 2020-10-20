using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Fans")]
    public class Fans:BasicsBase
    {
        public Fans()
        {
            User = new List<FansAndUser>();
        }
        public User Users { get; set; }
        public List<FansAndUser> User { get; set; }
    }
}
