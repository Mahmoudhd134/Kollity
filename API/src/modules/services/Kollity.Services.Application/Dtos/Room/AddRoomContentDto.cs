﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Kollity.Services.Application.Dtos.Room;

public class AddRoomContentDto
{
    [Required] public string Name { get; set; }
    [Required] public IFormFile File { get; set; }
}