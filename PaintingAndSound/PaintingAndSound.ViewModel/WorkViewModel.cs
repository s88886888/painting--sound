using PaintingAndSound.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel
{
    public class WorkViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDelete { get; set; }
        //public int UserId { get; set; }
        //public int PaintingId { get; set; }
        //public int RadioId { get; set; }
        public Radio Radios { get; set; }
        public Painting Paintings { get; set; }

    }
}
