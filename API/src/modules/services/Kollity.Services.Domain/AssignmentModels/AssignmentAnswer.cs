﻿using Kollity.Services.Domain.AssignmentModels.AssignmentGroupModels;
using Kollity.Services.Domain.StudentModels;

namespace Kollity.Services.Domain.AssignmentModels;

public class AssignmentAnswer
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; }
    public DateTime UploadDate { get; set; }
    public string File { get; set; }
    public byte? Degree { get; set; }

    public Guid? StudentId { get; set; }
    public Student Student { get; set; }

    public Guid? AssignmentGroupId { get; set; }
    public AssignmentGroup AssignmentGroup { get; set; }
    public List<AssignmentAnswerDegree> GroupDegrees { get; set; }
}