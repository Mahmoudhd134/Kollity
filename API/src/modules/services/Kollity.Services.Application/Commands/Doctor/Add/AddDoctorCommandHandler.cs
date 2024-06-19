using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Doctor;
using Kollity.Services.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using Kollity.User.Contracts;
using MediatR;

namespace Kollity.Services.Application.Commands.Doctor.Add;

public class AddDoctorCommandHandler : ICommandHandler<AddDoctorCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly EventCollection _eventCollection;
    private readonly IUserServiceServices _userServiceServices;

    public AddDoctorCommandHandler(ApplicationDbContext context, IMapper mapper, ISender sender,
        EventCollection eventCollection, IUserServiceServices userServiceServices)
    {
        _context = context;
        _mapper = mapper;
        _sender = sender;
        _eventCollection = eventCollection;
        _userServiceServices = userServiceServices;
    }

    public async Task<Result> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
    {
        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.AddDoctorDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return DoctorErrors.UserNameAlreadyExists(request.AddDoctorDto.UserName);

        var doctor = _mapper.Map<Domain.DoctorModels.Doctor>(request.AddDoctorDto);
        doctor.NormalizedUserName = doctor.UserName?.ToUpper();

        try
        {
            doctor.UserType = request.AddDoctorDto.Role switch
            {
                Role.Assistant => UserType.Assistant,
                Role.Doctor => UserType.Doctor,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        catch (ArgumentOutOfRangeException)
        {
            return RoleErrors.RoleNotFound(request.AddDoctorDto.Role);
        }

        _context.Doctors.Add(doctor);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        var addResult =
            await _userServiceServices.AddDoctor(doctor, request.AddDoctorDto.Password, request.AddDoctorDto.Role);
        if (addResult.IsSuccess == false)
            return addResult.Errors;

        _eventCollection.Raise(new DoctorAddedEvent(doctor, request.AddDoctorDto.Password, request.AddDoctorDto.Role));

        return Result.Success();
    }
}