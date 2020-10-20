using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.Entities
{
  public  class FansAndUser
    {
        public int FansId { get; set; }
        public int UserId { get; set; }
        public User  User { get; set; }
        public Fans  Fans { get; set; }
    }

}
