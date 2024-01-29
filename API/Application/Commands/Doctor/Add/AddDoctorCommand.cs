using Application.Dtos.Doctor;

namespace Application.Commands.Doctor.Add;

public record AddDoctorCommand(AddDoctorDto AddDoctorDto) : ICommand;