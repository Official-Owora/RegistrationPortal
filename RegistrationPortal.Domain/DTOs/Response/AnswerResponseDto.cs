namespace RegistrationPortal.Domain.DTOs.Response
{
    public record AnswerResponseDto
    {
        public QuestionResponseDto? question { get; init; }
        public string? applicantId { get; init; }
        public CustomQuestionResponseDto customQuestion { get; init; }
        public ICollection<ChoiceResponseDto> Choices { get; init; }
    }
}
