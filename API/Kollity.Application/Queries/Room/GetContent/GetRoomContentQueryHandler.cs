using AutoMapper.QueryableExtensions;
using Kollity.Application.Dtos.Room;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Room.GetContent;

public class GetRoomContentQueryHandler : IQueryHandler<GetRoomContentQuery, List<RoomContentDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetRoomContentQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<RoomContentDto>>> Handle(GetRoomContentQuery request,
        CancellationToken cancellationToken)
    {
        var content = await _context.RoomContents
            .Where(x => x.RoomId == request.RoomId)
            .ProjectTo<RoomContentDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return content;
    }
}