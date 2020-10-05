using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("Fans")]
    public class Fans:BasicsBase
    {
        public int UserId { get; set; }
        public User Users { get; set; }

    }
}
