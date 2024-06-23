namespace Kollity.Exams.Application.Dtos.Exam;

public class ExamQuestionForUserReviewDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int OpenForSeconds { get; set; }
    public byte Degree { get; set; }
    public List<ExamQuestionOptionForUserReviewDto> Options { get; set; }
    public Guid? OptionIdChosenByUser { get; set; }
}