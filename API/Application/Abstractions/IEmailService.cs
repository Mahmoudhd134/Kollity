using Application.Dtos.Email;

namespace Application.Abstractions;

public interface IEmailService
{
    Task<bool> TrySendAsync(EmailData emailData);
}