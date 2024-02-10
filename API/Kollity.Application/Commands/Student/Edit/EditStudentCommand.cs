using Kollity.Application.Dtos.Student;

namespace Kollity.Application.Commands.Student.Edit;

public record EditStudentCommand(EditStudentDto EditStudentDto) : ICommand;