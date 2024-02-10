using Kollity.Application.Dtos.Room;
using Kollity.Domain.RoomModels;

namespace Kollity.Application.MappingProfiles;

public class UserRoomMapsProfile : Profile
{
    public UserRoomMapsProfile()
    {
        CreateMap<UserRoom, RoomMemberDto>()
            .ForMember(d => d.State, opt =>
                opt.MapFrom(s => s.JoinRequestAccepted ? UserRoomState.Joined : UserRoomState.Pending))
            .ForMember(d => d.Id, opt =>
                opt.MapFrom(s => s.UserId))
            .ForMember(d => d.UserName, opt =>
                opt.MapFrom(s => s.User.UserName))
            .ForMember(d => d.ProfileImage, opt =>
                opt.MapFrom(s => s.User.ProfileImage));
    }
}