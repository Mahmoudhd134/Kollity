using Kollity.Application.Dtos.Doctor;

namespace Kollity.Application.Commands.Doctor.Add;

public record AddDoctorCommand(AddDoctorDto AddDoctorDto) : ICommand;