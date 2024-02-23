using Kollity.Application.Abstractions;
using Kollity.Application.Extensions;
using Kollity.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Doctor.Edit;

public class EditDoctorCommandHandler : ICommandHandler<EditDoctorCommand>
{
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly IUserAccessor _userAccessor;

    public EditDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager, IMapper mapper,
        ISender sender,
        IUserAccessor userAccessor)
    {
        _doctorManager = doctorManager;
        _mapper = mapper;
        _sender = sender;
        _userAccessor = userAccessor;
    }

    public async Task<Result> Handle(EditDoctorCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userAccessor.GetCurrentUserId();
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