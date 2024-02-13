using Kollity.Application.Dtos.Student;

namespace Kollity.Application.Queries.Student.Profile;

public record GetStudentProfileByIdQuery(Guid Id) : IQuery<StudentProfileDto>;