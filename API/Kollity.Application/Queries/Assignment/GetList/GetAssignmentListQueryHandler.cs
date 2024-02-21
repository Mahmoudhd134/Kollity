using AutoMapper.QueryableExtensions;
using Kollity.Application.Dtos.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetList;

public class GetAssignmentListQueryHandler : IQueryHandler<GetAssignmentListQuery, List<AssignmentForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentListQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<AssignmentForListDto>>> Handle(GetAssignmentListQuery request,
        CancellationToken cancellationToken)
    {
        var assignments = await _context.Assignments
            .Where(x => x.RoomId == request.RoomId)
            .ProjectTo<AssignmentForListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return assignments;
    }
}