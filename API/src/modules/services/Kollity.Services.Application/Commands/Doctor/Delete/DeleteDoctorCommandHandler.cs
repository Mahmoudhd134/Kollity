using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly EventCollection _eventCollection;
    private readonly IUserServiceServices _userServiceServices;

    public DeleteDoctorCommandHandler(ApplicationDbContext context, EventCollection eventCollection,IUserServiceServices userServiceServices)
    {
        _context = context;
        _eventCollection = eventCollection;
        _userServiceServices = userServiceServices;
    }

    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (doctor is null)
            return DoctorErrors.IdNotFound(request.Id);
        
        await _context.CourseAssistants
            .Where(x => x.AssistantId == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        var result = await _context.Doctors
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        if (result == 0)
            return Error.UnKnown;
        
        var r = await _userServiceServices.DeleteUser(doctor.Id);
        if (r.IsSuccess == false)
            return r.Errors;

        _eventCollection.Raise(new DoctorDeletedEvent(doctor));
        return Result.Success();
    }
}