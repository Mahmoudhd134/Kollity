using AutoMapper.QueryableExtensions;
using Kollity.Application.Dtos.Assignment;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Assignment.GetById;

public class GetAssignmentByIdQueryHandler : IQueryHandler<GetAssignmentByIdQuery, AssignmentDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAssignmentByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<AssignmentDto>> Handle(GetAssignmentByIdQuery request, CancellationToken cancellationToken)
    {
        var assignmentDto = await _context.Assignments
            .Where(x => x.Id == request.AssignmentId)
            .ProjectTo<AssignmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (assignmentDto is null)
            return AssignmentErrors.NotFound(request.AssignmentId);

        return assignmentDto;
    }
}