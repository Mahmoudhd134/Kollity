using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
using Kollity.Application.Dtos.Room;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Room.GetById;

public class GetRoomByIdQueryHandler : IQueryHandler<GetRoomByIdQuery, RoomDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetRoomByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Result<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var roomDto = await _context.Rooms
            .Where(x => x.Id == request.Id)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        if (roomDto is null)
            return RoomErrors.IdNotFound(request.Id);

        var id = _userAccessor.GetCurrentUserId();
        var userJoin = await _context.UserRooms
            .FirstOrDefaultAsync(x => x.RoomId == request.Id && x.UserId == id
                , cancellationToken);
        roomDto.UserState = UserRoomState.NotJoined;
        roomDto.UserState = userJoin switch
        {
            null => UserRoomState.NotJoined,
            { JoinRequestAccepted: false } => UserRoomState.Pending,
            { JoinRequestAccepted: true } => UserRoomState.Joined
        };
        return roomDto;
    }
}