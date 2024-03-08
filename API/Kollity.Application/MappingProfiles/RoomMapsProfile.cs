using Kollity.Application.Dtos.Identity;
using Kollity.Application.Dtos.Room;
using Kollity.Domain.RoomModels;

namespace Kollity.Application.MappingProfiles;

public class RoomMapsProfile : Profile
{
    public RoomMapsProfile()
    {
        CreateMap<EditRoomDto, Room>();
        CreateMap<Room, RoomDto>()
            .ForMember(d => d.Doctor, opt =>
                opt.MapFrom(s => s.DoctorId != null
                    ? new DoctorForRoomDto
                    {
                        Id = s.Doctor.Id,
                        UserName = s.Doctor.UserName,
                        ProfileImage = s.Doctor.ProfileImage
                    }
                    : null))
            .ForMember(d => d.Course, opt =>
                opt.MapFrom(s => new CourseForRoomDto
                {
                    Id = s.CourseId,
                    Name = s.Course.Name
                }));
        CreateMap<RoomContent, RoomContentDto>()
            .ForMember(d => d.Uploader, opt =>
                opt.MapFrom(s => new BaseUserDto
                {
                    Id = s.Uploader.Id,
                    ProfileImage = s.Uploader.ProfileImage,
                    UserName = s.Uploader.UserName
                }));
    }
}