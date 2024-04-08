using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Student.Delete;

public record DeleteStudentCommand(Guid Id) : ICommand;