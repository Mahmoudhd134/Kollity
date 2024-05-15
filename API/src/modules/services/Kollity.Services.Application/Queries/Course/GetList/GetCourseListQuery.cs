using Kollity.Services.Application.Dtos.Course;

namespace Kollity.Services.Application.Queries.Course.GetList;

public record GetCourseListQuery(CourseListFilters CourseListFilters) : IQuery<List<CourseForListDto>>;