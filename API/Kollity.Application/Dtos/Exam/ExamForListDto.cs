﻿namespace Kollity.Application.Dtos.Exam;

public class ExamForListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}