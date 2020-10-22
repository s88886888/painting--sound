using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.Entities
{
   public class PaintionPhotos:BasicsBase
    {
        public string ImagesUrl { get; set; }
        public int PaintingId { get; set; }
        public Painting Painting { get; set; }
    }
}
