﻿namespace Kollity.Application.Dtos.Identity;

public class BaseUserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string ProfileImage { get; set; }
}