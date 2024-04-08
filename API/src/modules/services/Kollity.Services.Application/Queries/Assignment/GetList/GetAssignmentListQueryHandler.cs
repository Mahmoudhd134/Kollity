using AutoMapper.QueryableExtensions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Assignment.GetList;

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