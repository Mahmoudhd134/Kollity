using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Commands.Exam.Add;

public record AddExamCommand(Guid RoomId, AddExamDto Dto) : ICommand<Guid>;