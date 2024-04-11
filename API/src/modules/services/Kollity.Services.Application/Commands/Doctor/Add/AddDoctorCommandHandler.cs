using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Services.Domain.Errors;
using MediatR;

namespace Kollity.Services.Application.Commands.Doctor.Add;

public class AddDoctorCommandHandler : ICommandHandler<AddDoctorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly EventCollection _eventCollection;

    public AddDoctorCommandHandler(ApplicationDbContext context, IMapper mapper, ISender sender,
        EventCollection eventCollection)
    {
        _context = context;
        _mapper = mapper;
        _sender = sender;
        _eventCollection = eventCollection;
    }

    public async Task<Result> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
    {
        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.AddDoctorDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return DoctorErrors.UserNameAlreadyExists(request.AddDoctorDto.UserName);

        var doctor = _mapper.Map<Domain.DoctorModels.Doctor>(request.AddDoctorDto);
        doctor.NormalizedUserName = doctor.UserName?.ToUpper();
        _context.Doctors.Add(doctor);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        _eventCollection.Raise(new DoctorAddedEvent(doctor, request.AddDoctorDto.Password, request.AddDoctorDto.Role));

        return Result.Success();
    }
}