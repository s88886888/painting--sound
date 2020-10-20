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

            //CreateMap<PaintingComment, PaintingCommentViewModel>();//PaintingCommentAutoMapper
            //CreateMap<PaintingCommentViewModel, PaintingComment>();

            CreateMap<Painting, PaintingViewModel>();//PaintingAutoMapper
            CreateMap<PaintingViewModel, Painting>();


            CreateMap<Works, WorkViewModel>();
            CreateMap<WorkViewModel, Works>();

        }
    }
}
