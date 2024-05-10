using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Room;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Room.Edit;

public class EditRoomCommandHandler : ICommandHandler<EditRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public EditRoomCommandHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(EditRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id == request.EditRoomDto.Id, cancellationToken);

        var id = _userServices.GetCurrentUserId();
        if (id != room.DoctorId)
            return RoomErrors.UnAuthorizeEdit;

        _mapper.Map(request.EditRoomDto, room);

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new RoomEditedEvent(room));

        return Result.Success();
    }
}