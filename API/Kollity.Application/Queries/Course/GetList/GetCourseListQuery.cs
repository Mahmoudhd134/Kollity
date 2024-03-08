using Kollity.Application.Dtos.Course;

namespace Kollity.Application.Queries.Course.GetList;

public record GetCourseListQuery(CourseListFilters CourseListFilters) : IQuery<List<CourseForListDto>>;