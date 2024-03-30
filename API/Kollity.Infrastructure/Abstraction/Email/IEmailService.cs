using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Infrastructure.Abstraction.Events;

namespace Kollity.Infrastructure.Abstraction.Email;

public interface IEmailService
{
    Task<bool> TrySendAsync(EmailData emailData);
    Task<List<bool>> TrySendAsync(List<EmailData> emailsData);

    Task<List<bool>> TrySendAssignmentGroupInvitationEmailsAsync(List<UserEmailDto> users,
        string creatorName,
        string roomName, string courseName, int code);

    Task<bool> TrySendConfirmationEmailAsync(string email, string token);
    Task<bool> TrySendResetPasswordEmailAsync(string email, string token);

    Task<List<bool>> TrySendNewAssignmentEmailAsync(List<UserEmailDto> users, string roomName,
        string courseName, string assignmentName,
        DateTime assignmentOpenUntil);

    Task<bool> TrySendAssignmentDegreeSetEmailAsync(UserEmailDto user, string assignmentName, byte degree);

    Task<List<bool>> TrySendExamAddedEmailAsync(string examName, DateTime examOpenDate, string roomName,
        string courseName, List<UserEmailDto> studentsInRoom);

    Task<List<bool>> TrySendRoomContentAddEmailAsync(string roomName, string contentName, DateTime addedAt,
        List<UserEmailDto> users);
}