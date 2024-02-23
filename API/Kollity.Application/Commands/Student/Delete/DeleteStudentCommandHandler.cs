using Kollity.Application.Extensions;
using Kollity.Domain.ErrorHandlers.Abstractions;
using Kollity.Domain.ErrorHandlers.Errors;
using Microsoft.AspNetCore.Identity;

namespace Kollity.Application.Commands.Student.Delete;

public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
{
    private readonly UserManager<Domain.StudentModels.Student> _studentManager;

    public DeleteStudentCommandHandler(UserManager<Domain.StudentModels.Student> studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentManager.FindByIdAsync(request.Id.ToString());

        if (student is null)
            return StudentErrors.IdNotFound(request.Id);

        var result = await _studentManager.DeleteAsync(student);

        return result.Succeeded ? Result.Success() : result.Errors.ToAppError().ToList();
    }
}