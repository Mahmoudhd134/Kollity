using AutoMapper;
using Kollity.Exams.Application.Dtos.Exam;
using Kollity.Exams.Domain.ExamModels;

namespace Kollity.Exams.Application.Helpers;

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

        CreateMap<AddExamQuestionDto, ExamQuestion>();
        CreateMap<EditExamQuestionDto, ExamQuestion>();
        CreateMap<AddExamQuestionOptionDto, ExamQuestionOption>();

        CreateMap<ExamQuestion, ExamQuestionDto>()
            .ForMember(d => d.Options, opt =>
                opt.MapFrom(s => s.ExamQuestionOptions.Select(x => new ExamQuestionOptionDto()
                {
                    Id = x.Id,
                    Option = x.Option,
                    IsRightOption = x.IsRightOption
                })));
    }
}