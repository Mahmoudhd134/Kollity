using Kollity.Contracts.Assignment;
using Kollity.Domain.AssignmentModels;

namespace Kollity.Application.Dtos.Assignment;

public class GroupingAssignmentAnswersDto
{
    public Guid AssignmentId { get; set; }
    public int NumberOfAnswers { get; set; }
    public AssignmentMode AssignmentMode { get; set; }
    public byte AssignmentDegree { get; set; }
    public string AssignmentName { get; set; }
    public List<GroupAssignmentAnswerDto> Groups { get; set; }
}