﻿using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Identity;

namespace Kollity.Services.Application.Commands.Identity.ResetPassword.Reset;

public record ResetPasswordCommand(ResetPasswordDto ResetPasswordDto) : ICommand;