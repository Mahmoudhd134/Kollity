using Kollity.Services.Domain.AssignmentModels;
using Kollity.Services.Application.Dtos.Assignment;

namespace Kollity.Services.Application.MappingProfiles;

public class AssignmentMapsProfile : Profile
{
    public AssignmentMapsProfile()
    {
        CreateMap<EditAssignmentDto, Assignment>();
        CreateMap<Assignment, AssignmentForListDto>()
            .ForMember(d => d.First150CharFromDescription, opt =>
                opt.MapFrom(s => s.Description.Substring(0, 150)));

        CreateMap<Assignment, AssignmentDto>()
            .ForMember(d => d.Files, opt =>
                opt.MapFrom(s => s.AssignmentFiles.Select(x => new AssignmentFileDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UploadDate = x.UploadDate
                })));
    }
}