using Application.Dtos.Student;

namespace Application.Commands.Student.Edit;

public record EditStudentCommand(EditStudentDto EditStudentDto) : ICommand;