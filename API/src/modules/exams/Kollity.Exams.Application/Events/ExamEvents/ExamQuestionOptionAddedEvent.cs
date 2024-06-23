﻿using Kollity.Exams.Application.Abstractions.Events;
using Kollity.Exams.Domain.ExamModels;

namespace Kollity.Exams.Application.Events.ExamEvents;

public record ExamQuestionOptionAddedEvent(ExamQuestionOption ExamQuestionOption) : IEvent;