using AutoMapper.QueryableExtensions;
using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Application.Dtos.Room;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Room.GetById;

public class GetRoomByIdQueryHandler : IQueryHandler<GetRoomByIdQuery, RoomDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public GetRoomByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public GetRoomByIdQueryHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result<RoomDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var roomDto = await _context.Rooms
            .Where(x => x.Id == request.Id)
            .ProjectTo<RoomDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        if (roomDto is null)
            return RoomErrors.NotFound(request.Id);

<<<<<<< HEAD
        var id = _userAccessor.GetCurrentUserId();
=======
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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