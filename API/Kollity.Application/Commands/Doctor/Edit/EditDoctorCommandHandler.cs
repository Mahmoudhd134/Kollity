using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;

    public EditDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager, IMapper mapper,
        ISender sender,
        IUserAccessor userAccessor)
=======
    private readonly IUserServices _userServices;

    public EditDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager, IMapper mapper,
        ISender sender,
        IUserServices userServices)
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    {
        _doctorManager = doctorManager;
        _mapper = mapper;
        _sender = sender;
<<<<<<< HEAD
        _userAccessor = userAccessor;
=======
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
    }

    public async Task<Result> Handle(EditDoctorCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        var currentUserId = _userAccessor.GetCurrentUserId();
=======
        var currentUserId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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