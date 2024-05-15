using System.Text.RegularExpressions;
using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Domain.Errors;
using Kollity.Services.Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Student.Add;

public class AddStudentCommandHandler : ICommandHandler<AddStudentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly EventCollection _eventCollection;
    private readonly ISender _sender;
    private readonly IUserServiceServices _userServiceServices;

    public AddStudentCommandHandler(ApplicationDbContext context, IMapper mapper,
        EventCollection eventCollection, ISender sender, IUserServiceServices userServiceServices)
    {
        _context = context;
        _mapper = mapper;
        _eventCollection = eventCollection;
        _sender = sender;
        _userServiceServices = userServiceServices;
    }

    public async Task<Result> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var passwordRegex = new Regex("^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$&*])(?=.*[0-9]).{8,}$");
        var isMatch = passwordRegex.IsMatch(request.AddStudentDto.Password);
        if (isMatch == false)
            return UserErrors.WeakPassword;

        var isUserNameUsed = await _context.Users
            .AnyAsync(x => x.NormalizedUserName == request.AddStudentDto.UserName.ToUpper(), cancellationToken);
        if (isUserNameUsed)
            return UserErrors.UserNameAlreadyUsed(request.AddStudentDto.UserName);

        var isCodeUsed = await _context.Students
            .AnyAsync(x => x.Code == request.AddStudentDto.Code, cancellationToken);
        if (isCodeUsed)
            return StudentErrors.CodeAlreadyExists(request.AddStudentDto.Code);

        var student = _mapper.Map<Domain.StudentModels.Student>(request.AddStudentDto);
        student.NormalizedUserName = request.AddStudentDto.UserName?.ToUpper();
        student.UserType = UserType.Student;
        _context.Students.Add(student);
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        var addResult = await _userServiceServices.AddStudent(student, request.AddStudentDto.Password);
        if (addResult.IsSuccess == false)
            return addResult.Errors;

        _eventCollection.Raise(new StudentAddedEvent(student, request.AddStudentDto.Password));

        return Result.Success();
    }
}