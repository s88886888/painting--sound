using PaintingAndSound.Entities;
using System;

namespace PaintingAndSound.ViewModel
{
    public class RadioViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public bool IsDelete { get; set; } = false;
        public string RadioUrl { get; set; }
        public int UserId { get; set; }
    }
}

