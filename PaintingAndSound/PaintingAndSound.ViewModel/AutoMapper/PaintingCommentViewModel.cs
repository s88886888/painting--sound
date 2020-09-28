using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel.AutoMapper
{
  public  class PaintingCommentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int PaintingId { get; set; }
        public DateTime dateTime { get; set; }

        public string Comments { get; set; }
        public int UserId { get; set; }


    }
}
