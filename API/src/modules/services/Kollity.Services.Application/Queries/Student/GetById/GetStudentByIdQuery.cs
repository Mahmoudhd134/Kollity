using Kollity.Services.Application.Dtos.Student;

namespace Kollity.Services.Application.Queries.Student.GetById;

public record GetStudentByIdQuery(Guid Id) : IQuery<StudentDto>;