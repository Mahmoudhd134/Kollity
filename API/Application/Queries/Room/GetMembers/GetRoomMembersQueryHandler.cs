using Application.Dtos.Room;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Room.GetMembers;

public class GetRoomMembersQueryHandler : IQueryHandler<GetRoomMembersQuery, List<RoomMemberDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRoomMembersQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<RoomMemberDto>>> Handle(GetRoomMembersQuery request,
        CancellationToken cancellationToken)
    {
        var members = await _context.UserRooms
            .Where(x => x.RoomId == request.RoomId)
            .ProjectTo<RoomMemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return members;
    }
}