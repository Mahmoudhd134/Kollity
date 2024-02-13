﻿using AutoMapper.QueryableExtensions;
using Kollity.Application.Dtos.Doctor;
using Kollity.Application.Dtos.Student;
using Kollity.Domain.DoctorModels;
using Kollity.Domain.StudentModels;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Application.Queries.Doctor.Profile;

public class GetDoctorProfileByIdQueryHandler : IQueryHandler<GetDoctorProfileByIdQuery, DoctorProfileDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDoctorProfileByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<DoctorProfileDto>> Handle(GetDoctorProfileByIdQuery request,
        CancellationToken cancellationToken)
    {
        var doctorProfileDto = await _context.Doctors
            .Where(x => x.Id == request.Id)
            .ProjectTo<DoctorProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (doctorProfileDto is null)
            return DoctorErrors.IdNotFound(request.Id);

        return doctorProfileDto;
    }
}