﻿using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Events;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.IntegrationEvents.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Commands.Exam.Add;

public class AddExamCommandHandler : ICommandHandler<AddExamCommand, Guid>
{
    private readonly ApplicationDbContext _context;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;

    public AddExamCommandHandler(ApplicationDbContext context, IUserServices userServices,
        EventCollection eventCollection)
    {
        _context = context;
        _userServices = userServices;
        _eventCollection = eventCollection;
    }

    public async Task<Result<Guid>> Handle(AddExamCommand request, CancellationToken cancellationToken)
    {
        Guid userId = _userServices.GetCurrentUserId(),
            roomId = request.RoomId;

        var room = await _context.Rooms
            .Where(x => x.Id == roomId)
            .Select(x => new
            {
                x.DoctorId,
                RoomName = x.Name,
                CourseName = x.Course.Name
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (room is null)
            return RoomErrors.NotFound(roomId);
        if (room.DoctorId is null)
            return RoomErrors.RoomHasNoDoctor;
        if (room.DoctorId != userId)
            return RoomErrors.UnAuthorizeAddExam;

        if (request.Dto.StartDate >= request.Dto.EndDate)
            return ExamErrors.StartDateCanNotBeAfterEndDate;

        var exam = new Domain.ExamModels.Exam()
        {
            RoomId = roomId,
            Name = request.Dto.Name,
            CreationDate = DateTime.UtcNow,
            StartDate = request.Dto.StartDate.ToUniversalTime(),
            EndDate = request.Dto.EndDate.ToUniversalTime(),
        };

        _context.Exams.Add(exam);

        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;
        _eventCollection.Raise(new ExamAddedEvent(exam));
        return exam.Id;
    }
}