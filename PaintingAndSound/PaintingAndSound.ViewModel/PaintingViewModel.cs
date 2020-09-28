using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel.AutoMapper
{
   public class PaintingViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string PaintingUrl { get; set; }

        public int UserId { get; set; }
    }
}
