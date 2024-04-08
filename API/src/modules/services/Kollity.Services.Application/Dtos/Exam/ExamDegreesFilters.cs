namespace Kollity.Services.Application.Dtos.Exam;

public class ExamDegreesFilters : PaginationDto
{
    public string FullName { get; set; }
    public string Code { get; set; }
    public bool WhoSolve { get; set; }
    public bool WhoDidNotSolve { get; set; }
}