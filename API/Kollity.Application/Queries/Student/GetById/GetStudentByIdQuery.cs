using Kollity.Application.Dtos.Student;

namespace Kollity.Application.Queries.Student.GetById;

public record GetStudentByIdQuery(Guid Id) : IQuery<StudentDto>;