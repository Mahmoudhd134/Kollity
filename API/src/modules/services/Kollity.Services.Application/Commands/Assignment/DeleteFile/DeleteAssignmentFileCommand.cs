using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Assignment.DeleteFile;

public record DeleteAssignmentFileCommand(Guid Id) : ICommand;