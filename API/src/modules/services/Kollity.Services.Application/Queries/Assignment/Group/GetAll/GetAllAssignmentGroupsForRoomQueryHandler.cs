using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment.Group;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Assignment.Group.GetAll;

public class
    GetAllAssignmentGroupsForRoomQueryHandler : IQueryHandler<GetAllAssignmentGroupsForRoomQuery,
    List<AssignmentGroupForListDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllAssignmentGroupsForRoomQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<AssignmentGroupForListDto>>> Handle(GetAllAssignmentGroupsForRoomQuery request,
        CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;

        var groups = await _context.AssignmentGroups
            .Where(x => x.RoomId == roomId)
            .Select(x => new AssignmentGroupForListDto
            {
                Id = x.Id,
                Code = x.Code,
                MembersCount = x.AssignmentGroupsStudents.Count(xx => xx.JoinRequestAccepted)
            })
            .ToListAsync(cancellationToken);
        return groups;
    }
}