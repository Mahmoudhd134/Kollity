﻿namespace Kollity.Reporting.Application.Queries.Course;

public class AssignmentQueryResult
{
    public int NumberOfAssignments { get; set; }
    public double? AvgAssignmentsDegree { get; set; }
    public int? SumOfAllDegrees { get; set; }
    public int? MaxSumStudentDegree { get; set; }
    public int? MinSumStudentDegree { get; set; }
    public int? NumberOfStudentsAnswers { get; set; }
}