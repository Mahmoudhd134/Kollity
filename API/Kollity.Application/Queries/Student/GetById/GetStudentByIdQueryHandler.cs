using AutoMapper.QueryableExtensions;
using Kollity.Application.Dtos.Student;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Student.GetById;

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