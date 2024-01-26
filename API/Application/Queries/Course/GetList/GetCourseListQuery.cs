using Application.Dtos.Course;

namespace Application.Queries.Course.GetList;

public record GetCourseListQuery(CourseListFilters CourseListFilters) : IQuery<List<CourseForListDto>>;