using Application.Abstractions;
using Application.Dtos.Email;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Infrastructure.Emails;

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
        catch (Exception ex)
        {
            return false;
        }
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
            MailKit.Security.SecureSocketOptions.StartTls);
        await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
        await mailClient.SendAsync(emailMessage);
        await mailClient.DisconnectAsync(true);
    }
}