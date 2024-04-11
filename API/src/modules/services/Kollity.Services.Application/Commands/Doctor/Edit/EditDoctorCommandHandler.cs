using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Services.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Doctor.Edit;

public class EditDoctorCommandHandler : ICommandHandler<EditDoctorCommand>
{
    private readonly EventCollection _eventCollection;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly IUserServices _userServices;
    private readonly ApplicationDbContext _context;

    public EditDoctorCommandHandler(EventCollection eventCollection, IMapper mapper,
        ISender sender,
        IUserServices userServices, ApplicationDbContext context)
    {
        _eventCollection = eventCollection;
        _mapper = mapper;
        _sender = sender;
        _userServices = userServices;
        _context = context;
    }

    public async Task<Result> Handle(EditDoctorCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userServices.GetCurrentUserId();
        if (currentUserId != request.EditDoctorDto.Id)
            return DoctorErrors.UnAuthorizeEdit;

        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.EditDoctorDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return UserErrors.UserNameAlreadyUsed(request.EditDoctorDto.UserName);

        var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == currentUserId, cancellationToken);
        if (doctor is null)
            return DoctorErrors.IdNotFound(currentUserId);


        _mapper.Map(request.EditDoctorDto, doctor);
        doctor.NormalizedUserName = doctor.UserName?.ToUpper();
        doctor.NormalizedEmail = doctor.Email?.ToUpper();
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new DoctorEditedEvent(doctor));
        return Result.Success();
    }
}