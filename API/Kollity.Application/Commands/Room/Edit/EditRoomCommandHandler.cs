﻿using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Room.Edit;

public class EditRoomCommandHandler : ICommandHandler<EditRoomCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserServices _userServices;

    public EditRoomCommandHandler(ApplicationDbContext context, IMapper mapper, IUserServices userServices)
    {
        _context = context;
        _mapper = mapper;
        _userServices = userServices;
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

        return Result.Success();
    }
}