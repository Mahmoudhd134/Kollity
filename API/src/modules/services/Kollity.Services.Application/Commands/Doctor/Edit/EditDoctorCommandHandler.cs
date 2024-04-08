using Kollity.Services.Application.Abstractions;
using Kollity.Services.Application.Extensions;
using Kollity.Services.Domain.ErrorHandlers.Abstractions;
using Kollity.Services.Domain.ErrorHandlers.Errors;
using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Abstractions.Services;
using Kollity.Services.Application.Queries.Identity.IsUserNameUsed;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Services.Application.Commands.Doctor.Edit;

public class EditDoctorCommandHandler : ICommandHandler<EditDoctorCommand>
{
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly IUserServices _userServices;

    public EditDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager, IMapper mapper,
        ISender sender,
        IUserServices userServices)
    {
        _doctorManager = doctorManager;
        _mapper = mapper;
        _sender = sender;
        _userServices = userServices;
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

        var doctor = await _doctorManager.FindByIdAsync(currentUserId.ToString());
        if (doctor is null)
            return DoctorErrors.IdNotFound(currentUserId);


        _mapper.Map(request.EditDoctorDto, doctor);
        var result = await _doctorManager.UpdateAsync(doctor);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}