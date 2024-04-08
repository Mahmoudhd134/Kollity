using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Doctor;

namespace Kollity.Services.Application.Commands.Doctor.Add;

public record AddDoctorCommand(AddDoctorDto AddDoctorDto) : ICommand;