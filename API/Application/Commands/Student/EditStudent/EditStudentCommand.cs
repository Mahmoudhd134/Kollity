using Application.Dtos.Student;

namespace Application.Commands.Student.EditStudent;

public record EditStudentCommand(Guid Id, EditStudentDto EditStudentDto) : ICommand;