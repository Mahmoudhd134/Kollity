using Kollity.Services.Application.Abstractions.Events;
using Kollity.Services.Application.Events.Student;
using Kollity.Services.Application.Queries.Identity.IsUserNameUsed;
using Kollity.Services.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kollity.Services.Application.Commands.Student.Edit;

public class EditStudentCommandHandler : ICommandHandler<EditStudentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    private readonly IUserServices _userServices;
    private readonly EventCollection _eventCollection;
    private readonly IUserServiceServices _userServiceServices;

    public EditStudentCommandHandler(ApplicationDbContext context,
        ISender sender,
        IMapper mapper,
        IUserServices userServices,
        EventCollection eventCollection,
        IUserServiceServices userServiceServices)
    {
        _context = context;
        _sender = sender;
        _mapper = mapper;
        _userServices = userServices;
        _eventCollection = eventCollection;
        _userServiceServices = userServiceServices;
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

        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == currentUserId, cancellationToken);
        if (student is null)
            return StudentErrors.IdNotFound(currentUserId);


        _mapper.Map(request.EditStudentDto, student);
        student.NormalizedUserName = student.UserName?.ToUpper();
        student.NormalizedEmail = student.NormalizedEmail?.ToUpper();
        var result = await _context.SaveChangesAsync(cancellationToken);
        if (result == 0)
            return Error.UnKnown;

        var r = await _userServiceServices.EditUser(student.Id, student.UserName);
        if (r.IsSuccess == false)
            return r.Errors;

        _eventCollection.Raise(new StudentEditedEvent(student));
        return Result.Success();
    }
}