namespace Kollity.Application.Commands.Doctor.Delete;

public record DeleteDoctorCommand(Guid Id) : ICommand;