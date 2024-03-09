using Kollity.Contracts.Dto;

namespace Kollity.Contracts.Events.Exam;

public record ExamAddedEvent(
    string ExamName,
    DateTime ExamOpenDate,
    Guid RoomId,
    string RoomName,
    string CourseName,
    List<UserEmailDto> StudentsInRoom)
    : IEvent;