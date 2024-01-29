using Application.Dtos.Doctor;

namespace Application.Commands.Doctor.Edit;

public record EditDoctorCommand(EditDoctorDto EditDoctorDto) : ICommand;