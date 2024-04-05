using Kollity.Application.Dtos.Exam;

namespace Kollity.Application.Commands.Exam.Add;

public record AddExamCommand(Guid RoomId, AddExamDto Dto) : ICommand<Guid>;