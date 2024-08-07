﻿using System.ComponentModel.DataAnnotations;

namespace Kollity.Services.Application.Dtos.Assignment.Group;

public class GroupAssignmentAnswersFilters
{
    public int? GroupCode { get; set; }
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
}