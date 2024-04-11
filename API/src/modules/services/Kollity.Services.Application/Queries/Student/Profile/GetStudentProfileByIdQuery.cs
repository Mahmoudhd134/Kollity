using Kollity.Services.Application.Dtos.Student;

namespace Kollity.Services.Application.Queries.Student.Profile;

public record GetStudentProfileByIdQuery(Guid Id) : IQuery<StudentProfileDto>;