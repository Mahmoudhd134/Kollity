namespace Kollity.Services.Contracts.Student;

public class StudentAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}