using Kollity.Application.Abstractions;
using Kollity.Application.Abstractions.Services;
using Kollity.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Student.Edit;

public class EditStudentCommandHandler : ICommandHandler<EditStudentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly UserManager<Domain.StudentModels.Student> _studentManager;
    private readonly IUserServices _userServices;

    public EditStudentCommandHandler(ApplicationDbContext context,
        ISender sender,
        IMapper mapper,
        IUserServices userServices,
        UserManager<Domain.StudentModels.Student> studentManager)
    {
        _context = context;
        _sender = sender;
        _mapper = mapper;
        _userServices = userServices;
        _studentManager = studentManager;
    }

    public async Task<Result> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _userServices.GetCurrentUserId();
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