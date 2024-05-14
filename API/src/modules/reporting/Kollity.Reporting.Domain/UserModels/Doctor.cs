using Kollity.Reporting.Domain.CourseModels;
using Kollity.Reporting.Domain.RoomModels;

namespace Kollity.Reporting.Domain.UserModels;

public class Doctor : User
{
    public DoctorType DoctorType { get; set; }
    public List<CourseDoctorAndAssistants> CourseDoctorAndAssistants { get; set; } = [];
    public List<Room> Rooms { get; set; }
}