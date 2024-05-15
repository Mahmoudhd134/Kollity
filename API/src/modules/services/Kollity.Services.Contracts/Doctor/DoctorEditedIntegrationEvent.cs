namespace Kollity.Services.Contracts.Doctor;

public class DoctorEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}