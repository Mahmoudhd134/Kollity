using Kollity.Application.Dtos.Email;

namespace Kollity.Application.Abstractions;

public interface IEmailService
{
    Task<bool> TrySendAsync(EmailData emailData);
}