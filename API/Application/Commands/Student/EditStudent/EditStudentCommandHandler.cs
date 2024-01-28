using Domain.StudentModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Student.EditStudent;

public class EditStudentCommandHandler : ICommandHandler<EditStudentCommand>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.StudentModels.Student> _studentManager;

    public EditStudentCommandHandler(ApplicationDbContext context,
        IMapper mapper,
        UserManager<Domain.StudentModels.Student> studentManager)
    {
        _context = context;
        _mapper = mapper;
        _studentManager = studentManager;
    }

    public async Task<Result> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        if (request.Id != request.EditStudentDto.Id)
            return StudentErrors.UnAuthorizeEdit;

        var student = await _studentManager.FindByIdAsync(request.Id.ToString());
        if (student is null)
            return StudentErrors.IdNotFound(request.Id);


        _mapper.Map(request.EditStudentDto, student);
        var result = await _studentManager.UpdateAsync(student);

        return result.Succeeded
            ? Result.Success()
            : result.Errors.Select(x => Error.Validation(x.Code, x.Description)).ToList();
    }
}