namespace Application.Commands.Student.AddStudent;

public record AddStudentCommand(AddStudentDto AddStudentDto) : ICommand;