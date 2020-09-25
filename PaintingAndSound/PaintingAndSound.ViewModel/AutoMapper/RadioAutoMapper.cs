using AutoMapper;
using PaintingAndSound.Entities;

namespace PaintingAndSound.ViewModel.AutoMapper
{
    public  class RadioAutoMapper:Profile
    {
        public RadioAutoMapper()
        {
            CreateMap<Radio, RadioViewModel>();
            CreateMap<RadioViewModel, Radio>();
        }
    }
}
