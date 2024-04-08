using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Commands.Exam.Question.Edit;

public record EditExamQuestionCommand(EditExamQuestionDto Dto) : ICommand;