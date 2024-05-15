using AutoMapper.QueryableExtensions;
using Kollity.Services.Application.Dtos.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Exam.GetList;

public class GetExamListQueryHandler : IQueryHandler<GetExamListQuery, List<ExamForListDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExamListQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Result<List<ExamForListDto>>> Handle(GetExamListQuery request,
        CancellationToken cancellationToken)
    {
        var exams = await _context.Exams
            .Where(x => x.RoomId == request.RoomId)
            .ProjectTo<ExamForListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return exams;
    }
}