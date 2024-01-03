using Application.Queries.Identity.IsEmailUsed;
using Application.Queries.Identity.IsUserNameUsed;
using AutoMapper;
using Domain.ErrorHandlers;
using Domain.Identity.Role;
using Domain.Identity.User;
using Domain.Student;
using MediatR;
using Persistence.Abstractions;

namespace Application.Commands.Student.AddStudent;

public class AddStudentCommandHandler : ICommandHandler<AddStudentCommand>
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISender _sender;

    public AddStudentCommandHandler(IStudentRepository studentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ISender sender, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
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
        var errors = await _studentRepository.CreateAsync(student);

        if (errors is not null && errors.Count > 0)
            return errors;

        await _userRepository.AddToRoleAsync(student, Role.Student);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}