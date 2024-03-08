using Kollity.Application.Dtos.Student;

namespace Kollity.Application.Commands.Student.Add;

public record AddStudentCommand(AddStudentDto AddStudentDto) : ICommand;