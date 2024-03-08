using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Edit;

public class EditRoomCommandHandler : ICommandHandler<EditRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public EditRoomCommandHandler(ApplicationDbContext context, IMapper mapper, IUserAccessor userAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userAccessor = userAccessor;
=======
    private readonly IUserServices _userServices;

    public EditRoomCommandHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(EditRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _context.Rooms
            .FirstOrDefaultAsync(x => x.Id == request.EditRoomDto.Id, cancellationToken);

<<<<<<< HEAD
        var id = _userAccessor.GetCurrentUserId();
=======
        var id = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        if (id != room.DoctorId)
            return RoomErrors.UnAuthorizeEdit;

        _mapper.Map(request.EditRoomDto, room);

        var result = await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}