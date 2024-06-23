using AutoMapper.QueryableExtensions;
using Kollity.Exams.Application.Dtos.Exam;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Exams.Application.Queries.GetList;

public class GetExamListQueryHandler : IQueryHandler<GetExamListQuery, List<ExamForListDto>>
{
    private readonly ExamsDbContext _context;
    private readonly IMapper _mapper;

    public GetExamListQueryHandler(ExamsDbContext context, IMapper mapper)
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