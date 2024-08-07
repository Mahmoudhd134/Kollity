﻿using AutoMapper.QueryableExtensions;
using Kollity.Services.Application.Dtos.Student;
using Kollity.Services.Domain.Errors;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Queries.Student.Profile;

public class GetStudentProfileByIdQueryHandler : IQueryHandler<GetStudentProfileByIdQuery, StudentProfileDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStudentProfileByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<StudentProfileDto>> Handle(GetStudentProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        var studentProfileDto = await _context.Students
            .Where(x => x.Id == request.Id)
            .ProjectTo<StudentProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (studentProfileDto is null)
            return StudentErrors.IdNotFound(request.Id);

        return studentProfileDto;
    }
}