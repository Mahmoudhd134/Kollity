using Application.Queries.Identity.IsEmailUsed;
using Application.Queries.Identity.IsUserNameUsed;
using AutoMapper;
using Domain.ErrorHandlers;
using Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Student.AddStudent;

public class AddStudentCommandHandler : ICommandHandler<AddStudentCommand>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly UserManager<Domain.Student.Student> _userManager;

    public AddStudentCommandHandler(UserManager<Domain.Student.Student> userManager, ISender sender, IMapper mapper)
    {
        _userManager = userManager;
        _sender = sender;
        _mapper = mapper;
    }

    public async Task<Result> Handle(AddStudentCommand request, CancellationToken cancellationToken)
    {
        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.AddStudentDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return UserErrors.UserNameAlreadyUsed(request.AddStudentDto.UserName);

        var isEmailUsed = await _sender.Send(
            new IsEmailUsedQuery(request.AddStudentDto.Email), cancellationToken);
        if (isEmailUsed.Data)
            return UserErrors.EmailAlreadyUsed(request.AddStudentDto.Email);

        var student = _mapper.Map<Domain.Student.Student>(request.AddStudentDto);
        var result = await _userManager.CreateAsync(student);

        if (result.Succeeded == false)
            return result.Errors
                .Select(x => new Error(x.Code, x.Description))
                .ToList();

        await _userManager.AddToRoleAsync(student, Role.Student);
        return Result.Success();
    }
}