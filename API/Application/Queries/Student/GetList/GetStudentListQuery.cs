using Application.Dtos.Student;

namespace Application.Queries.Student.GetList;

public record GetStudentListQuery(StudentListFilters Filters) : IQuery<List<StudentForListDto>>;