using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Student;

namespace Kollity.Services.Application.Commands.Student.Edit;

public record EditStudentCommand(EditStudentDto EditStudentDto) : ICommand;