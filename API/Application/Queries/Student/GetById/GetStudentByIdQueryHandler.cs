using Application.Dtos.Student;
using AutoMapper.QueryableExtensions;
using Domain.StudentModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Student.GetById;

public class GetStudentByIdQueryHandler : IQueryHandler<GetStudentByIdQuery, StudentDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var studentDto = await _context.Students
            .Where(x => x.Id == request.Id)
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (studentDto is null)
            return StudentErrors.IdNotFound(request.Id);

        return studentDto;
    }
}