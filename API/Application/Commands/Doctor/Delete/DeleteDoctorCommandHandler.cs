using Application.Extensions;
using Domain.DoctorModels;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctorCommand>
{
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;

    public DeleteDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager)
    {
        _doctorManager = doctorManager;
    }

    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorManager.FindByIdAsync(request.Id.ToString());

        if (doctor is null)
            return DoctorErrors.IdNotFound(request.Id);

        var result = await _doctorManager.DeleteAsync(doctor);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}