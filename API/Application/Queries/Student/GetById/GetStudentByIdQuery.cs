using Application.Dtos.Student;

namespace Application.Queries.Student.GetById;

public record GetStudentByIdQuery(Guid Id) : IQuery<StudentDto>;