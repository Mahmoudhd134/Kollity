﻿namespace Kollity.Exams.Contracts.Exam;

public class ExamEditedIntegrationEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}