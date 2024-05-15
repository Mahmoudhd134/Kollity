using Kollity.User.API.Abstraction.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Kollity.User.API.Services;

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
                        <a href="http://localhost:5196/api/identity/reset-password?email={email}&token={token}" target="_blank" style="width:64px;height:32px;border:1px solid black;border-radius:15px;background-color:blue;color:white;padding:10px;">
                        Reset Password
                        </a>
                        </div>
                        """
        });
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