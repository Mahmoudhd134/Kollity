namespace Kollity.Reporting.Domain.UserModels;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FullNameInArabic { get; set; }
    public string Code { get; set; }
    public string ProfileImage { get; set; }
    public UserType Type { get; set; }
    public bool IsDeleted { get; set; }
}