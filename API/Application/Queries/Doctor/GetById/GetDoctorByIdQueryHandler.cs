using Application.Dtos.Doctor;
using AutoMapper.QueryableExtensions;
using Domain.DoctorModels;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.Doctor.GetById;

public class GetDoctorByIdQueryHandler : IQueryHandler<GetDoctorByIdQuery, DoctorDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDoctorByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<DoctorDto>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
    {
        var doctorDto = await _context.Doctors
            .ProjectTo<DoctorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);


        return doctorDto is null ? DoctorErrors.IdNotFound(request.Id) : doctorDto;
    }
}