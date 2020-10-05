using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PaintingAndSound.Entities
{
    [Table("WorksComments")]
    public class WorksComments : BasicsBase
    {
        public User  User { get; set; }
        //public int WorksId { get; set; }
        public Works  Works { get; set; }
    }

}
