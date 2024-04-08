using Kollity.Services.Application.Abstractions.Messages;

namespace Kollity.Services.Application.Commands.Course.Delete;

public record DeleteCourseCommand(Guid Id) : ICommand;