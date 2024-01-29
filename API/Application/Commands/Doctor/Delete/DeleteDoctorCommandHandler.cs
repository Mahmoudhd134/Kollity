﻿using Application.Extensions;
using Domain.DoctorModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctorCommand>
{
    private readonly UserManager<Domain.DoctorModels.Doctor> _doctorManager;
    private readonly ApplicationDbContext _context;

    public DeleteDoctorCommandHandler(UserManager<Domain.DoctorModels.Doctor> doctorManager,
        ApplicationDbContext context)
    {
        _doctorManager = doctorManager;
        _context = context;
    }

    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorManager.FindByIdAsync(request.Id.ToString());

        if (doctor is null)
            return DoctorErrors.IdNotFound(request.Id);

        await _context.CourseAssistants
            .Where(x => x.AssistantId == request.Id)
            .ExecuteDeleteAsync(cancellationToken);
        
        var result = await _doctorManager.DeleteAsync(doctor);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}