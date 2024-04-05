﻿using Kollity.Contracts.Assignment;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class AssignmentReportDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public AssignmentMode Mode { get; set; }
    public byte Degree { get; set; }
    public int CountOfAnswers { get; set; }
    public List<StudentForAssignmentReportDto> Students { get; set; }
}