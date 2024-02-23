using Kollity.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Kollity.Domain.Identity.Role;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Doctor.Add;

public class AddDoctorCommandHandler : ICommandHandler<AddDoctorCommand>
{
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public AddDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager, IMapper mapper,
        ISender sender)
    {
        _doctorManager = doctorManager;
        _mapper = mapper;
        _sender = sender;
    }

    public async Task<Result> Handle(AddDoctorCommand request, CancellationToken cancellationToken)
    {
        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.AddDoctorDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return DoctorErrors.UserNameAlreadyExists(request.AddDoctorDto.UserName);

        var doctor = _mapper.Map<Domain.DoctorModels.Doctor>(request.AddDoctorDto);
        var result = await _doctorManager.CreateAsync(doctor, request.AddDoctorDto.Password);

        var errors = result.Errors
            .Select(e => Error.Validation(e.Code, e.Description))
            .ToList();

        if (errors.Count > 0)
            return errors;

        if (request.AddDoctorDto.Role == Role.Doctor)
            await _doctorManager.AddToRoleAsync(doctor, Role.Doctor);
        else if (request.AddDoctorDto.Role == Role.Assistant)
            await _doctorManager.AddToRoleAsync(doctor, Role.Assistant);

        return Result.Success();
    }
}