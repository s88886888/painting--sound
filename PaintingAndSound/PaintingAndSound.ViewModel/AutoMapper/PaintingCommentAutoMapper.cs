using AutoMapper;
using PaintingAndSound.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaintingAndSound.ViewModel.AutoMapper
{
  public  class PaintingCommentAutoMapper: Profile
    {
        public PaintingCommentAutoMapper()
        {
            CreateMap<PaintingComment, PaintingCommentViewModel>();
            CreateMap<PaintingCommentViewModel, PaintingComment>();
        }
    }
}
