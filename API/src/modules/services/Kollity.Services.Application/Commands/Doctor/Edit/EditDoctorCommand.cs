using Kollity.Services.Application.Dtos.Doctor;

namespace Kollity.Services.Application.Commands.Doctor.Edit;

public record EditDoctorCommand(EditDoctorDto EditDoctorDto) : ICommand;