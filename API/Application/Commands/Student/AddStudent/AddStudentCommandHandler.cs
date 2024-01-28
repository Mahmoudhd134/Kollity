using Application.Queries.Identity.IsUserNameUsed;
using Domain.Identity.Role;
using Domain.Identity.User;
using Domain.StudentModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Student.AddStudent;

public class AddStudentCommandHandler : ICommandHandler<AddStudentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly UserManager<Domain.StudentModels.Student> _studentManager;

    public AddStudentCommandHandler(ApplicationDbContext context, ISender sender, IMapper mapper,
        UserManager<Domain.StudentModels.Student> studentManager)
    {
        _context = context;
        _sender = sender;
        _mapper = mapper;
        _studentManager = studentManager;
    }

    public async Task<Result> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.AddStudentDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return UserErrors.UserNameAlreadyUsed(request.AddStudentDto.UserName);

        // var isEmailUsed = await _sender.Send(
        //     new IsEmailUsedQuery(request.AddStudentDto.Email), cancellationToken);
        // if (isEmailUsed.Data)
        //     return UserErrors.EmailAlreadyUsed(request.AddStudentDto.Email);

        var isCodeUsed = await _context.Students
            .AnyAsync(x => x.Code == request.AddStudentDto.Code, cancellationToken);
        if (isCodeUsed)
            return StudentErrors.CodeAlreadyExists(request.AddStudentDto.Code);

        var student = _mapper.Map<Domain.StudentModels.Student>(request.AddStudentDto);
        var result = await _studentManager.CreateAsync(student, request.AddStudentDto.Password);
        var errors = result.Errors
            .Select(e => Error.Validation(e.Code, e.Description))
            .ToList();

        if (errors.Count > 0)
            return errors;

        await _studentManager.AddToRoleAsync(student, Role.Student);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}