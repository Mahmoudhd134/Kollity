namespace Kollity.Services.Contracts.Student;

public class StudentEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}