using Kollity.Application.Dtos.Doctor;

namespace Kollity.Application.Commands.Doctor.Edit;

public record EditDoctorCommand(EditDoctorDto EditDoctorDto) : ICommand;