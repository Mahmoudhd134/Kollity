﻿using Kollity.Services.Application.Abstractions.Messages;
using Kollity.Services.Application.Dtos.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Exam.GetById;

public class GetExamByIdQueryHandler : IQueryHandler<GetExamByIdQuery, ExamDto>
{
    private readonly ApplicationDbContext _context;

    public GetExamByIdQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ExamDto>> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
    {
        var examId = request.ExamId;
        var examDto = await _context.Exams
            .Where(x => x.Id == examId)
            .Select(e => new ExamDto
            {
                Id = e.Id,
                Name = e.Name,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Questions = e.ExamQuestions.Select(q => new ExamQuestionDto
                {
                    Id = q.Id,
                    Question = q.Question,
                    Degree = q.Degree,
                    OpenForSeconds = q.OpenForSeconds,
                    Options = q.ExamQuestionOptions.Select(o => new ExamQuestionOptionDto
                    {
                        Id = o.Id,
                        Option = o.Option,
                        IsRightOption = o.IsRightOption
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (examDto is null)
            return ExamErrors.IdNotFound(examId);

        examDto.NumberOfQuestions = examDto.Questions.Count;
        examDto.Degree = examDto.Questions.Sum(x => x.Degree);
        examDto.TotalTime = examDto.Questions.Sum(x => x.OpenForSeconds);
        return examDto;
    }
}