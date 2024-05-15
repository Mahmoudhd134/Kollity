namespace Kollity.User.API.Abstraction.Services;

public interface IEmailService
{
    Task<bool> TrySendAsync(EmailData emailData);
    Task<List<bool>> TrySendAsync(List<EmailData> emailsData);
    Task<bool> TrySendResetPasswordEmailAsync(string email, string token);
    Task<bool> TrySendConfirmationEmailAsync(string email, string token);
}