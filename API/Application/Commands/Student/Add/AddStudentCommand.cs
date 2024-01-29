using Application.Dtos.Student;

namespace Application.Commands.Student.Add;

public record AddStudentCommand(AddStudentDto AddStudentDto) : ICommand;