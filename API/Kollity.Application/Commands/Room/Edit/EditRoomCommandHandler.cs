using Kollity.Application.Abstractions;
using Kollity.Domain.RoomModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Edit;

public class EditRoomCommandHandler : ICommandHandler<EditRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public EditRoomCommandHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(EditRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id == request.EditRoomDto.Id, cancellationToken);

        var id = _userAccessor.GetCurrentUserId();
        if (id != room.DoctorId)
            return RoomErrors.UnAuthorizeEdit;

        _mapper.Map(request.EditRoomDto, room);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}