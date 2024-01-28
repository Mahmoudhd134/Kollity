namespace Application.Commands.Student.DeleteStudent;

public record DeleteStudentCommand(Guid Id) : ICommand;