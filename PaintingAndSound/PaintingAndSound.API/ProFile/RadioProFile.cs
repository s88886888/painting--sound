using AutoMapper;
using PaintingAndSound.API.ViewModel;
using PaintingAndSound.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaintingAndSound.API.ProFile
{
    public class RadioProFile:Profile
    {
        public RadioProFile()
        {
            CreateMap<RadioViewModel, Radio>();
        }
    }
}
