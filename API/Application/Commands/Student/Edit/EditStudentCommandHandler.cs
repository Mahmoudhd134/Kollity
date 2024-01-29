using Application.Abstractions;
using Application.Queries.Identity.IsUserNameUsed;
using Domain.Identity.User;
using Domain.StudentModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Student.Edit;

public class EditStudentCommandHandler : ICommandHandler<EditStudentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly UserManager<Domain.StudentModels.Student> _studentManager;
    private readonly IUserAccessor _userAccessor;

    public EditStudentCommandHandler(ApplicationDbContext context,
        ISender sender,
        IMapper mapper,
        IUserAccessor userAccessor,
        UserManager<Domain.StudentModels.Student> studentManager)
    {
        _context = context;
        _sender = sender;
        _mapper = mapper;
        _userAccessor = userAccessor;
        _studentManager = studentManager;
    }

    public async Task<Result> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userAccessor.GetCurrentUserId();
        if (currentUserId != request.EditStudentDto.Id)
            return StudentErrors.UnAuthorizeEdit;

        var isUserNameUsed = await _sender.Send(
            new IsUserNameUsedQuery(request.EditStudentDto.UserName), cancellationToken);
        if (isUserNameUsed.Data)
            return UserErrors.UserNameAlreadyUsed(request.EditStudentDto.UserName);

        var student = await _studentManager.FindByIdAsync(currentUserId.ToString());
        if (student is null)
            return StudentErrors.IdNotFound(currentUserId);


        _mapper.Map(request.EditStudentDto, student);
        var result = await _studentManager.UpdateAsync(student);

        return result.Succeeded
            ? Result.Success()
            : result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();
    }
}