using Kollity.Application.Dtos.Assignment.Group;

namespace Kollity.Application.Commands.Assignment.AddFile;

public record AddAssignmentFileCommand(Guid Id, AddAssignmentFileDto AddAssignmentFileDto) : ICommand;