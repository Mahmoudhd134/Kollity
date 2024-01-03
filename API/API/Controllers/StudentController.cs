using Application.Commands.Student.AddStudent;
using Application.Dtos.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StudentController : BaseController
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public Task<IResult> Add(AddStudentDto addStudentDto) =>
        Send(new AddStudentCommand(addStudentDto));
}