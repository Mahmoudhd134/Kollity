﻿namespace Kollity.Application.Dtos.Identity;

public class ChangePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}