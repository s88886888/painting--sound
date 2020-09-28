using AutoMapper;
using PaintingAndSound.Entities;
using PaintingAndSound.ViewModel.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel
{
    public class PaintingAutoMapper: Profile
    {
        public PaintingAutoMapper()
        {
            CreateMap<Painting, PaintingViewModel>();
            CreateMap<PaintingViewModel, Painting>();
        }

    }
}
