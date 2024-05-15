using AutoMapper.QueryableExtensions;
using Kollity.Services.Application.Dtos.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Room.GetMembers;

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
            .Where(x => x.User.FullNameInArabic.StartsWith(request.Dto.FullName))
            .ProjectTo<RoomMemberDto>(_mapper.ConfigurationProvider)
            .Skip(request.Dto.PageIndex * request.Dto.PageSize)
            .Take(request.Dto.PageSize)
            .ToListAsync(cancellationToken);
        return members;
    }
}