﻿namespace Kollity.Services.Application.Commands.Exam.Question.Option.Delete;

public record DeleteExamQuestionOptionCommand(Guid OptionId) : ICommand;