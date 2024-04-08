using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Commands.Course.Add;

public record AddCourseCommand(AddCourseDto AddCourseDto) : ICommand;