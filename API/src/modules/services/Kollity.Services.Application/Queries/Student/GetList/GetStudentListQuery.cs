using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Student;

namespace Kollity.Services.Application.Queries.Student.GetList;

public record GetStudentListQuery(StudentListFilters Filters) : IQuery<List<StudentForListDto>>;