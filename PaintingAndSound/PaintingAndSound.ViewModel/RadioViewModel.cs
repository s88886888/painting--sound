using PaintingAndSound.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace PaintingAndSound.ViewModel
{
    public class RadioViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "FM名")]
        public string Name { get; set; }
        [Display(Name = "发布时间")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; } = false;
        [Required]
        [Display(Name = "路径")]
        public string Iamge { get; set; }

        public int UserId { get; set; }

       
    }
}

