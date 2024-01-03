using Application.Dtos.Student;

namespace Application.Commands.Student.AddStudent;

public record AddStudentCommand(AddStudentDto AddStudentDto) : ICommand;