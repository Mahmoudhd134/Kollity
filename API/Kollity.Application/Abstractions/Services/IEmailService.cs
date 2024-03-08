using Kollity.Application.Dtos.Assignment.Group;
using Kollity.Application.Dtos.Email;
using Kollity.Application.Dtos.Identity;

namespace Kollity.Application.Abstractions.Services;

public interface IEmailService
{
    Task<bool> TrySendAsync(EmailData emailData);
    Task<List<bool>> TrySendAsync(List<EmailData> emailsData);

    Task<List<bool>> TrySendAssignmentGroupInvitationEmailsAsync(List<UserEmailDto> users, string creatorName,
        string roomName,
        string courseName, int code);

    Task<bool> TrySendConfirmationEmailAsync(string email, string token);
    Task<bool> TrySendResetPasswordEmailAsync(string email, string token);

    Task<List<bool>> TrySendNewAssignmentEmailAsync(List<UserEmailDto> users, string roomName, string courseName,
        string assignmentName, DateTime assignmentOpenUntil);

    Task<bool> TrySendAssignmentDegreeSetEmailAsync(UserEmailDto user, string assignmentName, byte degree,
        DateTime degreeSetOn);
}