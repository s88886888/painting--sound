using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.Entities
{
  public  class RadioMusic:BasicsBase
    {
        public string RadioMusicUrl { get; set; }
        public string Detailed { get; set; }
        public string Image { get; set; }

        public int RadioId { get; set; }
        public Radio  Radio { get; set; }
    }
}
