﻿namespace Application.Commands.Student.Delete;

public record DeleteStudentCommand(Guid Id) : ICommand;