using Kollity.Services.Application.Dtos.Student;

namespace Kollity.Services.Application.Commands.Student.Add;

public record AddStudentCommand(AddStudentDto AddStudentDto) : ICommand, ITransactionalCommand;