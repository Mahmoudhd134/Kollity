using Kollity.Services.Application.Dtos.Exam;

namespace Kollity.Services.Application.Commands.Exam.Edit;

public record EditExamCommand(EditExamDto Dto) : ICommand;