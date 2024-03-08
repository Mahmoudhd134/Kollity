using Kollity.Application.Dtos.Student;

namespace Kollity.Application.Queries.Student.GetList;

public record GetStudentListQuery(StudentListFilters Filters) : IQuery<List<StudentForListDto>>;