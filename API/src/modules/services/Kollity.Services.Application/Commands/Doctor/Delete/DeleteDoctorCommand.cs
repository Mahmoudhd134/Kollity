namespace Kollity.Services.Application.Commands.Doctor.Delete;

public record DeleteDoctorCommand(Guid Id) : ICommand;