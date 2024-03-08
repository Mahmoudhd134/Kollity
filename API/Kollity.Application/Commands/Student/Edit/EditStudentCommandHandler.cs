using Kollity.Application.Abstractions;
<<<<<<< HEAD
=======
using Kollity.Application.Abstractions.Services;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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
<<<<<<< HEAD
    private readonly IUserAccessor _userAccessor;
=======
    private readonly IUserServices _userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e

    public EditStudentCommandHandler(ApplicationDbContext context,
        ISender sender,
        IMapper mapper,
<<<<<<< HEAD
        IUserAccessor userAccessor,
=======
        IUserServices userServices,
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        UserManager<Domain.StudentModels.Student> studentManager)
    {
        _context = context;
        _sender = sender;
        _mapper = mapper;
<<<<<<< HEAD
        _userAccessor = userAccessor;
=======
        _userServices = userServices;
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
        _studentManager = studentManager;
    }

    public async Task<Result> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        var currentUserId = _userAccessor.GetCurrentUserId();
=======
        var currentUserId = _userServices.GetCurrentUserId();
>>>>>>> 7034548f3e71eede6acd9fb1d886973eeab3616e
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