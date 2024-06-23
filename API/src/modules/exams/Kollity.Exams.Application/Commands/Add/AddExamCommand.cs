using Kollity.Exams.Application.Dtos.Exam;

namespace Kollity.Exams.Application.Commands.Add;

public record AddExamCommand(Guid RoomId, AddExamDto Dto) : ICommand<Guid>;