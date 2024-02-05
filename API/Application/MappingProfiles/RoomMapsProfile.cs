using Application.Dtos.Room;
using Domain.RoomModels;

namespace Application.MappingProfiles;

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
    }
}