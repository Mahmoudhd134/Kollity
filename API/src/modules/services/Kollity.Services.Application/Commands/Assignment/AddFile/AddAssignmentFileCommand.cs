using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Assignment.Group;

namespace Kollity.Services.Application.Commands.Assignment.AddFile;

public record AddAssignmentFileCommand(Guid Id, AddAssignmentFileDto AddAssignmentFileDto) : ICommand;