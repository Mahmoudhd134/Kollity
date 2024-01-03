using Domain.Identity;
using Domain.Identity.User;

namespace Domain.Student;

public class Student : BaseUser
{
    public string FullNameInArabic { get; set; }
}