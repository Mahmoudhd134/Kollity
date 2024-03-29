﻿using System.ComponentModel.DataAnnotations;

namespace Kollity.Application.Dtos.Doctor;

public class DoctorListFilters
{
    [Required] public int PageIndex { get; set; }
    [Required] public int PageSize { get; set; }
    public string UserNamePrefix { get; set; }
    public string Role { get; set; }
}