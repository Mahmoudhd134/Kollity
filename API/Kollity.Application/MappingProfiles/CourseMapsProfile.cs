using Kollity.Application.Dtos.Course;
using Kollity.Domain.CourseModels;

namespace Kollity.Application.MappingProfiles;

public class CourseMapsProfile : Profile
{
    public CourseMapsProfile()
    {
        CreateMap<AddCourseDto, Course>();
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.Doctor, opt =>
                opt.MapFrom(src => src.DoctorId != null
                    ? new DoctorForCourseDto
                    {
                        Id = src.Doctor.Id,
                        ProfileImage = src.Doctor.ProfileImage,
                        UserName = src.Doctor.UserName
                    }
                    : null))
            .ForMember(dest => dest.HasADoctor, opt =>
                opt.MapFrom(src => src.DoctorId != null))
            .ForMember(dest => dest.Assistants, opt =>
                opt.MapFrom(src => src.CoursesAssistants.Select(ca => new AssistantForCourseDto
                {
                    Id = ca.AssistantId,
                    UserName = ca.Assistant.UserName,
                    ProfileImage = ca.Assistant.ProfileImage
                })))
            .ForMember(dest => dest.Rooms, opt =>
                opt.MapFrom(src => src.Rooms.Select(r => new RoomForCourseDto
                {
                    Id = r.Id,
                    Image = r.Image,
                    Name = r.Name
                })));

        CreateMap<EditCourseDto, Course>();
        CreateMap<Course, CourseForListDto>();
    }
}