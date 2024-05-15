namespace Kollity.Services.Contracts.Doctor;

public class DoctorAddedIntegrationEvent
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DoctorType Type { get; set; }
}

public enum DoctorType
{
    Doctor = 1,
    Assistant = 2
}