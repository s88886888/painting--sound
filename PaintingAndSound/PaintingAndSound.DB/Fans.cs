using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.DB
{
  public class Fans:BasicsBase
    {

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public List<User> Users { get; set; }

    }
}
