﻿using Kollity.Application.Dtos.Exam;
using Kollity.Domain.ExamModels;

namespace Kollity.Application.MappingProfiles;

public class ExamMapsProfile : Profile
{
    public ExamMapsProfile()
    {
        CreateMap<Exam, ExamForListDto>();
        CreateMap<EditExamDto, Exam>()
            .ForMember(d => d.StartDate, o =>
                o.MapFrom(s => s.StartDate.ToUniversalTime()))
            .ForMember(d => d.EndDate, o =>
                o.MapFrom(s => s.EndDate.ToUniversalTime()));
    }
}