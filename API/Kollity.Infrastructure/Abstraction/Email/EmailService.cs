using Kollity.Application.IntegrationEvents.Dto;
using Kollity.Infrastructure.Abstraction.Events;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Kollity.Infrastructure.Abstraction.Email;

public class EmailService : IEmailService
{
    private readonly MailSettings _mailSettings;

    public EmailService(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }

    public async Task<bool> TrySendAsync(EmailData emailData)
    {
        try
        {
            await SendAsync(emailData);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<bool>> TrySendAsync(List<EmailData> emailsData)
    {
        return (await Task.WhenAll(emailsData.Select(TrySendAsync))).ToList();
    }

    public async Task<List<bool>> TrySendAssignmentGroupInvitationEmailsAsync(List<UserEmailDto> users,
        string creatorName,
        string roomName, string courseName, int code)
    {
        var creator = string.IsNullOrWhiteSpace(creatorName) ? "" : $"the creator is {creatorName}";
        var emails = users
            .Select(x => new EmailData()
            {
                ToName = x.FullName,
                ToEmail = x.Email,
                Subject = "Assignment Group Invitation",
                HtmlBody = $"""
                            <html>
                            <body>
                            <h1 style="text-align: center;">
                            Hi '{x.FullName}'
                            </h1>
                            <h3 style="text-align: center;">
                            You are invited to assignment group in {courseName} course
                            </h3>
                            <p style="text-align: center;">
                            the room name is {roomName}
                            <br/>
                            {creator}
                            <br/>
                            the group code is {code}
                            </p>
                            </body>
                            </html>
                            """
            })
            .ToList();

        return (await TrySendAsync(emails)).ToList();
    }

    public Task<bool> TrySendConfirmationEmailAsync(string email, string token)
    {
        return TrySendAsync(new EmailData
        {
            Subject = "Confirm Email",
            ToEmail = email,
            HtmlBody = $"""
                        <div  style="text-align:center;">
                        <h3>
                        Confirm Email
                        </h3>
                        <p>
                        click the button below to confirm your email
                        </p>
                        <a target="_blank" href="http://localhost:5196/api/identity/confirm-email?email={email}&token={token}"
                        <button>Confirm Email</button>
                        </a>
                        </div>
                        """
        });
    }

    public Task<bool> TrySendResetPasswordEmailAsync(string email, string token)
    {
        return TrySendAsync(new EmailData
        {
            Subject = "Reset Password",
            ToEmail = email,
            HtmlBody = $"""
                        <div  style="text-align:center;">
                        <h3>
                        Reset Password
                        </h3>
                        <p>
                        click the button below to reset your password
                        </p>
                        <a href="http://localhost:5196/api/identity/reset-password-2?email={email}&token={token}" target="_blank" style="width:64px;height:32px;border:1px solid black;border-radius:15px;background-color:blue;color:white;padding:10px;">
                        Reset Password
                        </a>
                        </div>
                        """
        });
    }

    public async Task<List<bool>> TrySendNewAssignmentEmailAsync(List<UserEmailDto> users, string roomName,
        string courseName, string assignmentName,
        DateTime assignmentOpenUntil)
    {
        return (await TrySendAsync(users.Select(x => new EmailData()
        {
            Subject = "A New Assignment Is Here",
            ToEmail = x.Email,
            ToName = x.FullName,
            HtmlBody = $"""
                        <html>
                        <body>
                        <h1 style="text-align: center;">
                        Hi '{x.FullName}'
                        </h1>
                        <h3 style="text-align: center;">
                        You have a new assignment
                        </h3>
                        <p style="text-align: center;">
                        the room name is {roomName}
                        <br/>
                        the course is {courseName}
                        <br/>
                        '{assignmentName}' is the new assignment and its open til {assignmentOpenUntil.ToLocalTime():yyyy-M-d dddd} {assignmentOpenUntil.ToLocalTime():h:mm:ss tt zz}
                        </p>
                        </body>
                        </html>
                        """
        }).ToList())).ToList();
    }

    public Task<bool> TrySendAssignmentDegreeSetEmailAsync(UserEmailDto user, string assignmentName, byte degree)
    {
        return TrySendAsync(new EmailData
        {
            Subject = "Assignment Degree Has Been Set",
            ToEmail = user.Email,
            HtmlBody = $"""
                        <h1 style="text-align: center;">
                        Hi '{user.FullName}'
                        </h1>
                        <h3 style="text-align: center;">
                        You have a new degree on assignment '{assignmentName}'
                        </h3>
                        <p style="text-align: center;">
                        '{degree}' is your degree on that assignment
                        <br/>
                        </p>
                        """
        });
    }

    public async Task<List<bool>> TrySendExamAddedEmailAsync(string examName, DateTime examOpenDate, string roomName,
        string courseName, List<UserEmailDto> studentsInRoom)
    {
        var emails = studentsInRoom
            .Select(x => new EmailData()
            {
                ToName = x.FullName,
                ToEmail = x.Email,
                Subject = "New Exam Is Here",
                HtmlBody = $"""
                            <html>
                            <body>
                            <h1 style="text-align: center;">
                            Hi '{x.FullName}'
                            </h1>
                            <h3 style="text-align: center;">
                            You have a new exam '{examName}' and it will open in  {examOpenDate.ToLocalTime():yyyy-M-d dddd} {examOpenDate.ToLocalTime():h:mm:ss tt zz}
                            </h3>
                            <p style="text-align: center;">
                            the course is {courseName}
                            <br/>
                            the room name is {roomName}
                            <br/>
                            </body>
                            </html>
                            """
            })
            .ToList();

        return (await TrySendAsync(emails)).ToList();
    }

    public async Task<List<bool>> TrySendRoomContentAddEmailAsync(string roomName, string contentName, DateTime addedAt,
        List<UserEmailDto> users)
    {
        var emails = users
            .Select(x => new EmailData()
            {
                ToName = x.FullName,
                ToEmail = x.Email,
                Subject = "New Material Is Here",
                HtmlBody = $"""
                            <html>
                            <body>
                            <h1 style="text-align: center;">
                            Hi '{x.FullName}'
                            </h1>
                            <h3 style="text-align: center;">
                            You have a new material '{contentName}' and it added at  {addedAt.ToLocalTime():yyyy-M-d dddd} {addedAt.ToLocalTime():h:mm:ss tt zz}
                            </h3>
                            <p style="text-align: center;">
                            the room name is {roomName}
                            <br/>
                            </body>
                            </html>
                            """
            })
            .ToList();

        return (await TrySendAsync(emails)).ToList();
    }

    private async Task SendAsync(EmailData emailData)
    {
        using var emailMessage = new MimeMessage();

        var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
        emailMessage.From.Add(emailFrom);

        var emailTo = new MailboxAddress(emailData.ToName, emailData.ToEmail);
        emailMessage.To.Add(emailTo);

        emailMessage.Subject = emailData.Subject;
        var emailBodyBuilder = new BodyBuilder
        {
            TextBody = emailData.TextBody,
            HtmlBody = emailData.HtmlBody
        };
        emailMessage.Body = emailBodyBuilder.ToMessageBody();

        using var mailClient = new SmtpClient();
        await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port,
            SecureSocketOptions.StartTls);
        await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
        await mailClient.SendAsync(emailMessage);
        await mailClient.DisconnectAsync(true);
    }
}