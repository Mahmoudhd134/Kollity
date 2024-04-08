using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Doctor.Delete;

public record DeleteDoctorCommand(Guid Id) : ICommand;