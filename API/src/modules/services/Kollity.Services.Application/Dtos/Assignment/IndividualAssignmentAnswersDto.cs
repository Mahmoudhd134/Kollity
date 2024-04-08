using Kollity.Services.Contracts.Assignment;
using Kollity.Services.Domain.AssignmentModels;

namespace Kollity.Services.Application.Dtos.Assignment;

public class IndividualAssignmentAnswersDto
{
    public Guid AssignmentId { get; set; }
    public int NumberOfAnswers { get; set; }
    public AssignmentMode AssignmentMode { get; set; }
    public byte AssignmentDegree { get; set; }
    public string AssignmentName { get; set; }
    public List<StudentAssignmentAnswerDto> Students { get; set; }
}